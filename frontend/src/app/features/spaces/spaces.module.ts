import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";

import {SpacesRoutingModule} from "./spaces-routing.module";
import {SharedModule} from "../../shared/shared.module";
import {SpacesComponent} from "./spaces.component";
import { AddSpaceComponent } from './containers/add-space/add-space.component';
import { AddSpaceFormComponent } from './components/add-space-form/add-space-form.component';


@NgModule({
  declarations: [
    SpacesComponent,
    AddSpaceComponent,
    AddSpaceFormComponent
  ],
  imports: [
    CommonModule,
    SpacesRoutingModule,
    SharedModule
  ]
})
export class SpacesModule {
}
