import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {AddProductCommand} from "../../shared/commands/add-product.command";
import {Observable} from "rxjs";

@Injectable({
  providedIn: "root"
})
export class ProductService {
  private readonly endpoint = "api/products";

  public constructor(private readonly http: HttpClient) {
  }

  public add(command: AddProductCommand): Observable<void> {
    return this.http.post<void>(this.endpoint, command);
  }
}
