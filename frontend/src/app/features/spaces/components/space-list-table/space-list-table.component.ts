import {ChangeDetectionStrategy, Component, EventEmitter, Input, Output, ViewChild} from "@angular/core";
import {Page} from "../../../../shared/models/page.model";
import {SpaceList} from "../../../../shared/models/space.model";
import {MatPaginator, PageEvent} from "@angular/material/paginator";
import {MatTableDataSource} from "@angular/material/table";

@Component({
  selector: "app-space-list-table",
  templateUrl: "./space-list-table.component.html",
  styleUrls: ["./space-list-table.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SpaceListTableComponent {
  @Input()
  public set spaces(spaces: SpaceList[] | null) {
    this.dataSource.data = spaces ?? [];
  }

  @Input()
  public set totalAmount(totalAmount: number | null) {
    if (this.paginator) {
      this.paginator.length = totalAmount ?? 0;
    }
  }

  @Input()
  public set currentPage(page: Page | null) {
    if (this.paginator && page) {
      this.paginator.pageIndex = page.pageIndex;
      this.paginator.pageSize = page.pageSize;
    }
  }

  @Output()
  public pageChanged = new EventEmitter<Page>();

  @ViewChild("paginator")
  public paginator: MatPaginator | null = null;

  public columns = ["id", "name"];
  public dataSource = new MatTableDataSource<SpaceList>([]);

  public changePage({pageIndex, pageSize}: PageEvent): void {
    this.pageChanged.emit({pageIndex, pageSize});
  }
}
