import {InjectionToken} from "@angular/core";
import {environment} from "../../../environments/environment";

export const BASE_URL = new InjectionToken<string>("Base Url", {
  providedIn: "root",
  factory: (): string => environment.apiUrl
});
