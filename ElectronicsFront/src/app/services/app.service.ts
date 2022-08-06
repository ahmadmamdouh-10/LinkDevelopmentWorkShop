import { environment } from './../../environments/environment';
import { IFooter } from './../models/IFooter.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
    providedIn : 'root'
})

export class AppService{

    constructor(private http:HttpClient) {
    }

    getAbout(storeId:number){
        return this.http.get<IFooter>(`${environment.baseUrl}app/aboutus?storeId=${storeId}`);
    }
    contact(name:string,email:string,message:string,storeId:number){
        return this.http.post<any>(`${environment.baseUrl}app/contactus`,{Name : name,Email :email, Body:message,StoreId:storeId});
    }
    loginBackground(storeId:number){
        return this.http.get<any>(`${environment.baseUrl}settings/AppLogInPageBackground?storeId=${storeId}`);
    }
    homeBackground(storeId:number){
        return this.http.get<any>(`${environment.baseUrl}settings/AppHomePageBackground?storeId=${storeId}`);
    }
    homeVideo(storeId:number){
        return this.http.get<any>(`${environment.baseUrl}settings/AppHomeVideo?storeId=${storeId}`);
    }
}
