import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {AddProductCommand, ChangeProductInformationCommand, IncreaseProductStockCommand} from "../../shared/commands/product.command";
import {Observable} from "rxjs";
import {GetProductsQuery, GetProductsQueryResult} from "../../shared/queries/product.query";

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

  public get({pageIndex, pageSize}: GetProductsQuery): Observable<GetProductsQueryResult> {
    const params = new HttpParams().appendAll({pageIndex, pageSize});

    return this.http.get<GetProductsQueryResult>(this.endpoint, {params});
  }

  public changeInformation(command: ChangeProductInformationCommand): Observable<void> {
    const url = `${this.endpoint}/change-information`;

    return this.http.post<void>(url, command);
  }

  public increaseStock(command: IncreaseProductStockCommand): Observable<void> {
    const url = `${this.endpoint}/increase-stock`;

    return this.http.post<void>(url, command);
  }

  public decreaseStock(command: IncreaseProductStockCommand): Observable<void> {
    const url = `${this.endpoint}/decrease-stock`;

    return this.http.post<void>(url, command);
  }

  public remove(id: string): Observable<void> {
    const url = `${this.endpoint}/${id}`;
    
    return this.http.delete<void>(url);
  }
}
