import {ProductList} from "../models/product.model";

export type GetProductsQuery = {
  readonly pageIndex: number;
  readonly pageSize: number;
};

export type GetProductsQueryResult = {
  readonly products: ProductList[];
  readonly totalAmount: number;
}
