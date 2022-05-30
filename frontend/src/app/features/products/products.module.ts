import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";

import {ProductsRoutingModule} from "./products-routing.module";
import {ProductsComponent} from "./products.component";
import {SharedModule} from "../../shared/shared.module";
import {AddProductComponent} from "./containers/add-product/add-product.component";
import {AddProductFormComponent} from "./components/add-product-form/add-product-form.component";


@NgModule({
  declarations: [
    ProductsComponent,
    AddProductComponent,
    AddProductFormComponent
  ],
  imports: [
    CommonModule,
    ProductsRoutingModule,
    SharedModule
  ]
})
export class ProductsModule {
}
