import {ChangeDetectionStrategy, Component, EventEmitter, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Guid} from "../../../../shared/models/guid.model";
import {AddProductCommand} from "../../../../shared/commands/add-product.command";

@Component({
  selector: "app-add-product-form",
  templateUrl: "./add-product-form.component.html",
  styleUrls: ["./add-product-form.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddProductFormComponent {
  @Output()
  public productAdded = new EventEmitter<AddProductCommand>();

  public form: FormGroup;

  public constructor(formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      name: [null, [Validators.required, Validators.maxLength(50)]],
      price: [null, [Validators.required, Validators.min(0)]],
      stock: [null, [Validators.required, Validators.min(0)]]
    });
  }

  public addProduct(): void {
    const command: AddProductCommand = {
      id: Guid.New(),
      name: this.form.get("name")?.value,
      price: this.form.get("price")?.value,
      stock: this.form.get("stock")?.value
    };

    this.productAdded.emit(command);
  }
}
