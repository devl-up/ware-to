import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";

import {SpacesRoutingModule} from "./spaces-routing.module";
import {SharedModule} from "../../shared/shared.module";
import {SpacesComponent} from "./spaces.component";
import {AddSpaceComponent} from "./containers/add-space/add-space.component";
import {AddSpaceFormComponent} from "./components/add-space-form/add-space-form.component";
import {SpaceListComponent} from "./containers/space-list/space-list.component";
import {SpaceListTableComponent} from "./components/space-list-table/space-list-table.component";


@NgModule({
  declarations: [
    SpacesComponent,
    AddSpaceComponent,
    AddSpaceFormComponent,
    SpaceListComponent,
    SpaceListTableComponent
  ],
  imports: [
    CommonModule,
    SpacesRoutingModule,
    SharedModule
  ]
})
export class SpacesModule {
}
