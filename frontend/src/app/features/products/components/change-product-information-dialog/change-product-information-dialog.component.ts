import {ChangeDetectionStrategy, Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ChangeProductInformationCommand} from "../../../../shared/commands/product.command";

export type ChangeProductInformationDialogData = {
  readonly id: string;
  readonly name: string;
  readonly price: number;
}

@Component({
  selector: "app-change-product-information-dialog",
  templateUrl: "./change-product-information-dialog.component.html",
  styleUrls: ["./change-product-information-dialog.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChangeProductInformationDialogComponent {
  public form: FormGroup;

  public constructor(
    @Inject(MAT_DIALOG_DATA) public data: ChangeProductInformationDialogData,
    public readonly dialog: MatDialogRef<ChangeProductInformationDialogComponent>,
    formBuilder: FormBuilder
  ) {
    this.form = formBuilder.group({
      name: [data.name, [Validators.required, Validators.maxLength(50)]],
      price: [data.price, [Validators.required, Validators.min(0)]]
    });
  }

  public save(): void {
    const command: ChangeProductInformationCommand = {
      id: this.data.id,
      name: this.form.get("name")?.value,
      price: this.form.get("price")?.value
    };

    this.dialog.close(command)
  }
}
