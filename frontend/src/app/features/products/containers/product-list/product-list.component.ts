import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from "@angular/core";
import {BehaviorSubject, Observable, Subject, Subscription, switchMap, tap} from "rxjs";
import {ProductList} from "../../../../shared/models/product.model";
import {ProductService} from "../../../../core/services/product.service";
import {GetProductsQuery} from "../../../../shared/queries/product.query";
import {Page} from "../../../../shared/models/page.model";

@Component({
  selector: "app-product-list",
  templateUrl: "./product-list.component.html",
  styleUrls: ["./product-list.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductListComponent implements OnInit, OnDestroy {
  private _subscriptions = new Subscription();
  private _getProducts = new Subject<GetProductsQuery>();
  private _products = new BehaviorSubject<ProductList[]>([]);
  private _totalAmount = new BehaviorSubject<number>(0);

  public products$: Observable<ProductList[]>;
  public totalAmount$: Observable<number>;

  public constructor(productService: ProductService) {
    this.products$ = this._products.asObservable();
    this.totalAmount$ = this._totalAmount.asObservable();

    const subscription = this._getProducts.pipe(
      switchMap(query => productService.get(query)),
      tap(result => {
        this._products.next(result.products);
        this._totalAmount.next(result.totalAmount);
      })
    ).subscribe();

    this._subscriptions.add(subscription);
  }

  public getProducts({pageIndex, pageSize}: Page): void {
    this._getProducts.next({pageIndex, pageSize});
  }

  public ngOnInit(): void {
    this.getProducts({pageIndex: 0, pageSize: 10});
  }

  public ngOnDestroy(): void {
    this._subscriptions.unsubscribe();
  }
}
