import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from "@angular/core";
import {BehaviorSubject, combineLatest, filter, mergeMap, Observable, of, Subject, Subscription, switchMap, tap} from "rxjs";
import {ProductList} from "../../../../shared/models/product.model";
import {ProductService} from "../../../../core/services/product.service";
import {GetProductsQuery} from "../../../../shared/queries/product.query";
import {Page} from "../../../../shared/models/page.model";
import {MatDialog} from "@angular/material/dialog";
import {ChangeProductInformationDialogComponent} from "../../components/change-product-information-dialog/change-product-information-dialog.component";
import {ProductStockVariation, ProductStockVariationDialogComponent, ProductStockVariationDialogData} from "../../components/product-stock-variation-dialog/product-stock-variation-dialog.component";
import {ChangeProductInformationCommand} from "../../../../shared/commands/product.command";
import {RemoveProductDialogComponent} from "../../components/remove-product-dialog/remove-product-dialog.component";

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductListComponent implements OnInit, OnDestroy {
  private _subscriptions = new Subscription();

  private _getProducts = new Subject<GetProductsQuery>();
  private _openChangeInformation = new Subject<ProductList>();
  private _openIncreaseStock = new Subject<ProductList>();
  private _openDecreaseStock = new Subject<ProductList>();
  private _openRemoveProduct = new Subject<ProductList>();

  private _products = new BehaviorSubject<ProductList[]>([]);
  private _totalAmount = new BehaviorSubject<number>(0);
  private _currentPage = new BehaviorSubject<Page>({pageIndex: 0, pageSize: 10});

  public products$: Observable<ProductList[]>;
  public totalAmount$: Observable<number>;
  public currentPage$: Observable<Page>;

  public constructor(productService: ProductService, private readonly matDialog: MatDialog) {
    this.products$ = this._products.asObservable();
    this.totalAmount$ = this._totalAmount.asObservable();
    this.currentPage$ = this._currentPage.asObservable();

    const getProductsSubscription = this._getProducts.pipe(
      switchMap(query => combineLatest([productService.get(query), of(query)])),
      tap(([result, {pageIndex, pageSize}]) => {
        this._currentPage.next({pageIndex, pageSize});
        this._products.next(result.products);
        this._totalAmount.next(result.totalAmount);
      })
    ).subscribe();

    this._subscriptions.add(getProductsSubscription);

    const openChangeInformationSubscription = this._openChangeInformation.pipe(
      switchMap(product => this.matDialog.open(ChangeProductInformationDialogComponent, {data: product}).afterClosed()),
      filter((command: ChangeProductInformationCommand) => !!command),
      mergeMap(command => productService.changeInformation(command)),
      tap(() => this.getProducts(this._currentPage.value)),
    ).subscribe();

    this._subscriptions.add(openChangeInformationSubscription);

    const openIncreaseStockSubscription = this._openIncreaseStock.pipe(
      switchMap(product => {
        const data: ProductStockVariationDialogData = {
          id: product.id,
          type: "Increase",
          currentStockValue: product.stock
        };

        return this.matDialog.open(ProductStockVariationDialogComponent, {data}).afterClosed();
      }),
      filter((variationAmount: ProductStockVariation) => !!variationAmount),
      mergeMap(({id, amount}) => productService.increaseStock({id, variation: amount})),
      tap(() => this.getProducts(this._currentPage.value)),
    ).subscribe();

    this._subscriptions.add(openIncreaseStockSubscription);

    const openDecreaseStockSubscription = this._openDecreaseStock.pipe(
      switchMap(product => {
        const data: ProductStockVariationDialogData = {
          id: product.id,
          type: "Decrease",
          currentStockValue: product.stock
        };

        return this.matDialog.open(ProductStockVariationDialogComponent, {data}).afterClosed();
      }),
      filter((variationAmount: ProductStockVariation) => !!variationAmount),
      mergeMap(({id, amount}) => productService.decreaseStock({id, variation: amount})),
      tap(() => this.getProducts(this._currentPage.value)),
    ).subscribe();

    this._subscriptions.add(openDecreaseStockSubscription);

    const openRemoveProductSubscription = this._openRemoveProduct.pipe(
      switchMap(product => this.matDialog.open(RemoveProductDialogComponent, {data: product}).afterClosed()),
      filter((id: string) => !!id),
      mergeMap(id => productService.remove(id)),
      tap(() => this.getProducts(this._currentPage.value))
    ).subscribe();

    this._subscriptions.add(openRemoveProductSubscription);
  }

  public getProducts({pageIndex, pageSize}: Page): void {
    this._getProducts.next({pageIndex, pageSize});
  }

  public openChangeInformation(product: ProductList): void {
    this._openChangeInformation.next(product);
  }

  public openIncreaseStock(product: ProductList): void {
    this._openIncreaseStock.next(product);
  }

  public openDecreaseStock(product: ProductList): void {
    this._openDecreaseStock.next(product);
  }

  public openRemoveProduct(product: ProductList): void {
    this._openRemoveProduct.next(product);
  }

  public ngOnInit(): void {
    this.getProducts(this._currentPage.value);
  }

  public ngOnDestroy(): void {
    this._subscriptions.unsubscribe();
  }
}
