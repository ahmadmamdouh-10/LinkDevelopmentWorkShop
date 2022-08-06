import { Offer } from './../models/offer.model';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn : 'root'
})
export class OfferService{

    constructor(private http:HttpClient) {

    }
    offers(storeId:number,lang:string){
        return this.http.get<Offer[]>(`${environment.baseUrl}offers?storeId=${storeId}&lang=${lang}`);
    }
}
