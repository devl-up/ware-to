import {Injectable} from "@angular/core";
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {catchError, EMPTY, Observable} from "rxjs";
import {MatSnackBar} from "@angular/material/snack-bar";
import {ErrorSnackbarComponent} from "../../shared/components/error-snackbar/error-snackbar.component";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  public constructor(private readonly snackBar: MatSnackBar) {
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(catchError((response: HttpErrorResponse) => {
      this.snackBar.openFromComponent(ErrorSnackbarComponent, {
        data: response.error.errors,
        duration: 3000,
        panelClass: "snackbar-color",
        horizontalPosition: "end",
        verticalPosition: "top"
      })

      return EMPTY;
    }));
  }
}
