import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from "@angular/core";
import {
  BehaviorSubject,
  combineLatest,
  filter,
  mergeMap,
  Observable,
  of,
  Subject,
  Subscription,
  switchMap,
  tap
} from "rxjs";
import {ProductList} from "../../../../shared/models/product.model";
import {ProductService} from "../../../../core/services/product.service";
import {GetProductsQuery} from "../../../../shared/queries/product.query";
import {Page} from "../../../../shared/models/page.model";
import {MatDialog} from "@angular/material/dialog";
import {
  ChangeInformationDialogComponent
} from "../../components/change-information-dialog/change-information-dialog.component";

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
      switchMap(product => this.matDialog.open(ChangeInformationDialogComponent, {data: product}).afterClosed()),
      filter(command => !!command),
      mergeMap(command => productService.changeInformation(command)),
      tap(() => this.getProducts(this._currentPage.value)),
    ).subscribe();

    this._subscriptions.add(openChangeInformationSubscription);
  }

  public getProducts({pageIndex, pageSize}: Page): void {
    this._getProducts.next({pageIndex, pageSize});
  }

  public openChangeInformation(product: ProductList): void {
    this._openChangeInformation.next(product);
  }

  public ngOnInit(): void {
    this.getProducts(this._currentPage.value);
  }

  public ngOnDestroy(): void {
    this._subscriptions.unsubscribe();
  }
}
