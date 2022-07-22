import {SpaceList} from "../models/space.model";

export type GetSpacesQuery = {
  readonly pageIndex: number;
  readonly pageSize: number;
};

export type GetSpacesQueryResult = {
  readonly spaces: SpaceList[];
  readonly totalAmount: number;
}
