import {NgModule} from "@angular/core";
import {RouterModule, Routes} from "@angular/router";

const routes: Routes = [
  {path: "products", loadChildren: () => import("./features/products/products.module").then(m => m.ProductsModule)},
  {path: "spaces", loadChildren: () => import("./features/spaces/spaces.module").then(m => m.SpacesModule)},
  {path: "", redirectTo: "products", pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
