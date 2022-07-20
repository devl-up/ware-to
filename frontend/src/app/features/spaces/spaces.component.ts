import {ChangeDetectionStrategy, Component, ViewChild} from "@angular/core";
import {MatTabGroup} from "@angular/material/tabs";

@Component({
  selector: "app-spaces",
  templateUrl: "./spaces.component.html",
  styleUrls: ["./spaces.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpacesComponent {
  @ViewChild(MatTabGroup)
  public tabGroup: MatTabGroup | null = null;

  public navigateToTab(index: number): void {
    if (this.tabGroup) {
      this.tabGroup.selectedIndex = index;
    }
  }
}
