import {ChangeDetectionStrategy, Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ProductList} from "../../../../shared/models/product.model";

@Component({
  selector: "app-remove-product-dialog",
  templateUrl: "./remove-product-dialog.component.html",
  styleUrls: ["./remove-product-dialog.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RemoveProductDialogComponent {
  public constructor(@Inject(MAT_DIALOG_DATA) public data: ProductList, public readonly dialog: MatDialogRef<RemoveProductDialogComponent>) {
  }

  public confirm(): void {
    this.dialog.close(this.data.id);
  }
}
