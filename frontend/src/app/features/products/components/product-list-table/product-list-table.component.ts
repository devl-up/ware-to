import {ChangeDetectionStrategy, Component, EventEmitter, Input, Output, ViewChild} from "@angular/core";
import {ProductList} from "../../../../shared/models/product.model";
import {Page} from "../../../../shared/models/page.model";
import {MatTableDataSource} from "@angular/material/table";
import {MatPaginator, PageEvent} from "@angular/material/paginator";

@Component({
  selector: "app-product-list-table",
  templateUrl: "./product-list-table.component.html",
  styleUrls: ["./product-list-table.component.scss"],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ProductListTableComponent {
  @Input()
  public set products(products: ProductList[] | null) {
    this.dataSource.data = products ?? [];
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

  @Output()
  public changeInformationOpened = new EventEmitter<ProductList>();

  @Output()
  public stockIncreaseOpened = new EventEmitter<ProductList>();

  @Output()
  public stockDecreaseOpened = new EventEmitter<ProductList>();

  @Output()
  public removeProductOpened = new EventEmitter<ProductList>();

  @ViewChild("paginator")
  public paginator: MatPaginator | null = null;

  public columns = ["id", "name", "price", "stock", "actions"];
  public dataSource = new MatTableDataSource<ProductList>([]);

  public changePage({pageIndex, pageSize}: PageEvent): void {
    this.pageChanged.emit({pageIndex, pageSize});
  }

  public openChangeInformation(product: ProductList): void {
    this.changeInformationOpened.emit(product);
  }

  public openStockIncrease(product: ProductList): void {
    this.stockIncreaseOpened.emit(product);
  }

  public openStockDecrease(product: ProductList): void {
    this.stockDecreaseOpened.emit(product);
  }

  public openRemoveProduct(product: ProductList): void {
    this.removeProductOpened.emit(product);
  }
}
