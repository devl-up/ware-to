import {ChangeDetectionStrategy, Component, OnDestroy} from "@angular/core";
import {mergeMap, Subject, Subscription} from "rxjs";
import {AddSpaceCommand} from "../../../../shared/commands/space.command";
import {SpaceService} from "../../../../core/services/space.service";

@Component({
  selector: "app-add-space",
  templateUrl: "./add-space.component.html",
  styleUrls: ["./add-space.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AddSpaceComponent implements OnDestroy {
  private _subscription = new Subscription();
  private _addSpace = new Subject<AddSpaceCommand>();

  public constructor(spaceService: SpaceService) {
    const subscription = this._addSpace.pipe(
      mergeMap(command => spaceService.add(command)),
    ).subscribe();

    this._subscription.add(subscription);
  }

  public addSpace(command: AddSpaceCommand): void {
    this._addSpace.next(command);
  }

  public ngOnDestroy(): void {
    this._subscription.unsubscribe();
  }
}
