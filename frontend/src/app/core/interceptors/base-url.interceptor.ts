import {Inject, Injectable} from "@angular/core";
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import {Observable} from "rxjs";
import {BASE_URL} from "../tokens/base-url.token";

@Injectable()
export class BaseUrlInterceptor implements HttpInterceptor {
  public constructor(@Inject(BASE_URL) private readonly baseUrl: string) {
  }

  public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.baseUrl) {
      const splitUrl = req.url.split("/");

      if (splitUrl[0] === "api") {
        req = req.clone({
          url: `${this.baseUrl}/${req.url}`
        });
      }
    }
    return next.handle(req);
  }
}
