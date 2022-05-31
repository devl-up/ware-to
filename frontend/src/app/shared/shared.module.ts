import {NgModule} from "@angular/core";
import {CommonModule} from "@angular/common";
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatListModule} from "@angular/material/list";
import {MatTabsModule} from "@angular/material/tabs";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {MatSnackBarModule} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "./components/error-snackbar/error-snackbar.component";

const material = [
  MatToolbarModule,
  MatSidenavModule,
  MatListModule,
  MatTabsModule,
  MatFormFieldModule,
  MatInputModule,
  MatButtonModule,
  MatSnackBarModule
];

const angular = [
  FormsModule,
  ReactiveFormsModule,
]

const components = [
  ErrorSnackbarComponent
]

@NgModule({
  imports: [
    CommonModule,
    ...angular,
    ...material
  ],
  exports: [
    ...angular,
    ...material
  ],
  declarations: [
    ...components
  ]
})
export class SharedModule {
}
