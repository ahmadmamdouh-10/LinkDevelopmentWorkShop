import { IStore } from './IStore.model';
export interface IAddressModel {
    street: string;
}

export interface UserAddress {
    Street: string;
    Store:IStore;
    Id:number,
    IsDefault:boolean
}

export interface INewAddress{
    Id:number,
    Street: string,
    UserId: number,
    IsDefault: boolean,
    StoreId: number
  }
