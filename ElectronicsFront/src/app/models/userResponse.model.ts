import { IStore } from './IStore.model';

export interface UserResponse {
    User:any;
    IsExist:boolean;
    Stores :IStore[];
    UserId:number;
}