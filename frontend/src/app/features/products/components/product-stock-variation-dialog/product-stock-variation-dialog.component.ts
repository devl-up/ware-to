import {ChangeDetectionStrategy, Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";

export type ProductStockVariationDialogData = {
  readonly id: string;
  readonly type: "Increase" | "Decrease";
  readonly currentStockValue: number;
}

export type ProductStockVariation = {
  readonly id: string;
  readonly amount: number;
}

@Component({
  selector: "app-product-stock-variation-dialog",
  templateUrl: "./product-stock-variation-dialog.component.html",
  styleUrls: ["./product-stock-variation-dialog.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductStockVariationDialogComponent {
  public form: FormGroup;

  public constructor(
    @Inject(MAT_DIALOG_DATA) public data: ProductStockVariationDialogData,
    public readonly dialog: MatDialogRef<ProductStockVariationDialogComponent>,
    formBuilder: FormBuilder
  ) {
    this.form = formBuilder.group({
      variation: [null, [Validators.required, Validators.min(1)]]
    });
  }

  public save(): void {
    const variation: ProductStockVariation = {
      id: this.data.id,
      amount: this.form.get("variation")?.value
    };

    this.dialog.close(variation);
  }
}
