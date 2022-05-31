import {ChangeDetectionStrategy, Component, ViewChild} from "@angular/core";
import {MatTabGroup} from "@angular/material/tabs";

@Component({
  selector: "app-products",
  templateUrl: "./products.component.html",
  styleUrls: ["./products.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductsComponent {
  @ViewChild(MatTabGroup)
  public tabGroup: MatTabGroup | null = null;

  public navigateToTab(index: number): void {
    if (this.tabGroup) {
      this.tabGroup.selectedIndex = index;
    }
  }
}
