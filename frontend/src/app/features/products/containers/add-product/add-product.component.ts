import {ChangeDetectionStrategy, Component, OnDestroy} from "@angular/core";
import {AddProductCommand} from "../../../../shared/commands/add-product.command";
import {mergeMap, Subject, Subscription} from "rxjs";
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

  public constructor(productService: ProductService) {
    const subscription = this._addProduct.pipe(
      mergeMap(command => productService.add(command))
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
