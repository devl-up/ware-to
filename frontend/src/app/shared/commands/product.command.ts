export type AddProductCommand = {
  readonly id: string;
  readonly name: string;
  readonly price: number;
  readonly stock: number;
}

export type ChangeProductInformationCommand = {
  readonly id: string;
  readonly name: string;
  readonly price: number;
}
