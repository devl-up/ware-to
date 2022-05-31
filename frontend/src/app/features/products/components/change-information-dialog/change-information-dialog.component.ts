import {ChangeDetectionStrategy, Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ProductList} from "../../../../shared/models/product.model";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ChangeProductInformationCommand} from "../../../../shared/commands/product.command";

@Component({
  selector: "app-change-information-dialog",
  templateUrl: "./change-information-dialog.component.html",
  styleUrls: ["./change-information-dialog.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ChangeInformationDialogComponent {
  public form: FormGroup;

  public constructor(@Inject(MAT_DIALOG_DATA) public data: ProductList, formBuilder: FormBuilder, public readonly dialog: MatDialogRef<ChangeInformationDialogComponent>) {
    this.form = formBuilder.group({
      name: [data.name, [Validators.required, Validators.maxLength(50)]],
      price: [data.price, [Validators.required, Validators.min(0)]],
      stock: [data.stock]
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
