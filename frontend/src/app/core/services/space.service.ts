import {Injectable} from "@angular/core";
import {HttpClient, HttpParams} from "@angular/common/http";
import {AddSpaceCommand} from "../../shared/commands/space.command";
import {Observable} from "rxjs";
import {GetSpacesQuery, GetSpacesQueryResult} from "../../shared/queries/space.query";

@Injectable({
  providedIn: "root"
})
export class SpaceService {
  private readonly endpoint = "api/spaces";

  public constructor(private readonly http: HttpClient) {
  }

  public add(command: AddSpaceCommand): Observable<void> {
    return this.http.post<void>(this.endpoint, command);
  }

  public get({pageIndex, pageSize}: GetSpacesQuery): Observable<GetSpacesQueryResult> {
    const params = new HttpParams().appendAll({pageIndex, pageSize});

    return this.http.get<GetSpacesQueryResult>(this.endpoint, {params});
  }
}
