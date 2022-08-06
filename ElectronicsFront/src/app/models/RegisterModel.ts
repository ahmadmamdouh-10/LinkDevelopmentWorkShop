import { IAddressModel } from './IAddressModel';
export class RegisterModel {
    email: string | undefined;
    password: string | undefined;
    name: string | undefined;
    deviceToken: string = 'browser';
    gender: number | undefined;
    phone: string | undefined;
    storeId: number | undefined;
    code!: string;
    address!: IAddressModel;
}
