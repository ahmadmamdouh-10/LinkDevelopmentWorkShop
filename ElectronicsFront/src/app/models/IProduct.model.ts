import { IStore } from './IStore.model';
export interface IProduct{
    Id: number,
    Title: string,
    Rate: number,
    InStock: boolean,
    ImageUrl: string,
    Discount: number,
    Description: string,
    Price: number,
    Code: string,
    Store:IStore,
    Category:string
}

export interface IProductDetails{
    Id: number,
    Title: string,
    Description: string,
    Rate: number,
    Discount: number,
    FavoriteCount: number,
    Code: string,
    InStock: boolean,
    Images: string[],
    Category: string,
    Price: number,
    Store:IStore,
}
