import { environment } from './../../environments/environment';
import { NewOrder, IOrder, IOrderProducts } from './../models/IOrder.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";

@Injectable({
    providedIn :'root'
})

export class OrderService{

    constructor(private http:HttpClient){
    }

    saveOrder(newOrder:NewOrder){
        return this.http.post<any>(`${environment.baseUrl}order`,newOrder);
    }
    getOrder(userId:any,storeId:any){
        return this.http.get<IOrder[]>(`${environment.baseUrl}order?userId=${userId}&storeId=${storeId}`);
    }
    getOrderProducts(orderId:any,storeId:any,lang= 'en'){
        return this.http.get<IOrderProducts>(`${environment.baseUrl}order/order-products?orderId=${orderId}&storeId=${storeId}&lang=${lang}`);
    }
}
