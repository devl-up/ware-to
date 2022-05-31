import {v4} from "uuid";

export class Guid {
  public static New(): string {
    return v4();
  }
}
