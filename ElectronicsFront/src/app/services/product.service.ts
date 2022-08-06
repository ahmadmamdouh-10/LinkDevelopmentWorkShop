import { environment } from './../../environments/environment';
import { IProduct, IProductDetails } from './../models/IProduct.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
    providedIn:'root'
})
export class ProductService{

  private apiData = new BehaviorSubject<any>(null);
  public apiData$ = this.apiData.asObservable();

    constructor(private http:HttpClient) {
    }

    setData(data:any){
      this.apiData.next(data);
    }

    latestProducts(storeId:number){
        return this.http.get<IProduct[]>(`${environment.baseUrl}product/latestProducts?storeId=${storeId}`);
    }
    getById(id:number,storeId:number,userId:any){
        return this.http.get<IProductDetails>(`${environment.baseUrl}product/getById?productId=${id}&storeId=${storeId}&userId=${userId}`);
    }
    getByCategory(category:string,storeId:number){
        return this.http.get<IProductDetails[]>(`${environment.baseUrl}product/getByCategory?category=${category.replace('&','%26')}&storeId=${storeId}`);
    }
}
