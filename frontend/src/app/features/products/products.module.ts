import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";

import {ProductsRoutingModule} from "./products-routing.module";
import {ProductsComponent} from "./products.component";
import {SharedModule} from "../../shared/shared.module";
import {AddProductComponent} from "./containers/add-product/add-product.component";
import {AddProductFormComponent} from "./components/add-product-form/add-product-form.component";
import {ProductListComponent} from "./containers/product-list/product-list.component";
import {ProductListTableComponent} from "./components/product-list-table/product-list-table.component";
import {ChangeProductInformationDialogComponent} from "./components/change-product-information-dialog/change-product-information-dialog.component";
import {ProductStockVariationDialogComponent} from "./components/product-stock-variation-dialog/product-stock-variation-dialog.component";
import {RemoveProductDialogComponent} from "./components/remove-product-dialog/remove-product-dialog.component";


@NgModule({
  declarations: [
    ProductsComponent,
    AddProductComponent,
    AddProductFormComponent,
    ProductListComponent,
    ProductListTableComponent,
    ChangeProductInformationDialogComponent,
    ProductStockVariationDialogComponent,
    RemoveProductDialogComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    SharedModule
  ]
})
export class ProductsModule {
}
