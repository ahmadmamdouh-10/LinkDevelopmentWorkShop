import { IStore } from './IStore.model';

export interface IOrder{
        Id: number,
        Rate: number,
        Status: string,
        Total: number,
        TotalDiscount: number,
        TotalAfterDiscount: number,
        DeliveryAddress: string,
        Contact: string,
        Notes: string,
        CreatedDate: Date,
        OrderDetails: [
          {
            ProductId: number,
            ProductName: string,
            Price: number,
            Amount: number
          }
        ],
        Store: IStore

}

export class OrderDet{
    ProductId: number | undefined;
    Amount: number | undefined;
}
export class NewOrder {
        OrderDetails:OrderDet[] = [];
        UserId: number | undefined;
        StoreId: number | undefined;
        AddressId: number | undefined;
        Notes: string | undefined
}

export interface IOrderProducts{
    Id: number,
    Rate: number,
    Status: number,
    Total: number,
    TotalDiscount: number,
    TotalAfterDiscount: number,
    DeliveryAddress: string,
    Contact: string,
    Notes: string,
    CreatedDate: Date,
    Products: [
      {
        Amount: number,
        Id: number,
        Title: string,
        Description: string,
        Rate: number,
        Discount: number,
        Code: string,
        InStock: true,
        Images: [
          string
        ],
        Category: string,
      }
    ],
    Store: IStore
}
