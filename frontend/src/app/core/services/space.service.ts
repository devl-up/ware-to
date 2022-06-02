import {Injectable} from "@angular/core";
import {HttpClient} from "@angular/common/http";
import {AddSpaceCommand} from "../../shared/commands/space.command";
import {Observable} from "rxjs";

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
}
