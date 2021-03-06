import {ChangeDetectionStrategy, Component, EventEmitter, OnDestroy, Output} from "@angular/core";
import {AddProductCommand} from "../../../../shared/commands/product.command";
import {mergeMap, Subject, Subscription, tap} from "rxjs";
import {ProductService} from "../../../../core/services/product.service";

@Component({
  selector: "app-add-product",
  templateUrl: "./add-product.component.html",
  styleUrls: ["./add-product.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddProductComponent implements OnDestroy {
  private _subscription = new Subscription();
  private _addProduct = new Subject<AddProductCommand>();

  @Output()
  public productAdded = new EventEmitter();

  public constructor(productService: ProductService) {
    const subscription = this._addProduct.pipe(
      mergeMap(command => productService.add(command)),
      tap(() => this.productAdded.emit())
    ).subscribe();

    this._subscription.add(subscription);
  }

  public addProduct(command: AddProductCommand): void {
    this._addProduct.next(command);
  }

  public ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }
}
