import {ChangeDetectionStrategy, Component, EventEmitter, Output} from "@angular/core";
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {Guid} from "../../../../shared/models/guid.model";
import {AddSpaceCommand} from "../../../../shared/commands/space.command";

@Component({
  selector: "app-add-space-form",
  templateUrl: "./add-space-form.component.html",
  styleUrls: ["./add-space-form.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddSpaceFormComponent {
  @Output()
  public spaceAdded = new EventEmitter<AddSpaceCommand>();

  public form: FormGroup;

  public constructor(formBuilder: FormBuilder) {
    this.form = formBuilder.group({
      name: [null, [Validators.required, Validators.maxLength(50)]]
    });
  }

  public addSpace(): void {
    const command: AddSpaceCommand = {
      id: Guid.New(),
      name: this.form.get("name")?.value
    };

    this.spaceAdded.emit(command);
  }
}
