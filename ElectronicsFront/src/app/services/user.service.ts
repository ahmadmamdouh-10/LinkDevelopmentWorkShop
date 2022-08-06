import { GlobalService } from './global.service';
import { INewAddress } from './../models/IAddressModel';
import { UserResponse } from './../models/userResponse.model';
import { RegisterModel } from "./../models/RegisterModel";
import { environment } from './../../environments/environment';
import { Injectable } from "@angular/core";
import { Observable, BehaviorSubject } from 'rxjs';
import { LoginModel } from '../models/login.model';
import { HttpClient } from '@angular/common/http';
import { UserAddress } from '../models/IAddressModel';
import { Router } from '@angular/router';
import { IStore } from '../models/IStore.model';

@Injectable({
    providedIn : 'root'
})
export class UserService{
    currentUser:BehaviorSubject<any> =
        new BehaviorSubject<any>(null);

    currentLang:BehaviorSubject<string> =
    new BehaviorSubject<any>('en');

    currentStoreName :BehaviorSubject<string> =
        new BehaviorSubject<string>('');

    currentStoreLogo :BehaviorSubject<string> =
        new BehaviorSubject<string>('');

    constructor(private http:HttpClient,
                private router:Router,
                private gService:GlobalService) {
        var user = localStorage.getItem('electronicsUser');
        var storeName = localStorage.getItem('electronicsStoreName');
        var storeLogo = localStorage.getItem('electronicsStoreLogo');
        if(user){
            this.currentUser.next(JSON.parse(user));
        }
        if(storeName)
            this.currentStoreName.next(storeName);
        if(storeLogo)
            this.currentStoreName.next(storeLogo);
    }

    isLoggedIn(){
        return this.currentUser.value != null
    }

    login(loginModel:LoginModel) :Observable<UserResponse>{
        return this.http.post<UserResponse>(`${environment.baseUrl}user/login`,loginModel);
    }

    register(registerModel:RegisterModel) : Observable<UserResponse>{
        return this.http.post<UserResponse>(`${environment.baseUrl}user/registration`,registerModel);
    }
    logout(){
        localStorage.removeItem('electronicsUser');
        localStorage.removeItem('electronicsStoreName');
        localStorage.removeItem('electronicsStoreLogo');
        this.currentUser.next(null);
        this.currentStoreName.next('');
        this.currentStoreLogo.next('');
        this.gService.store()
        .subscribe((res:IStore) =>{
          localStorage.setItem('electronicsUser',res.Id.toString());
          localStorage.setItem('electronicsStoreName',res.Name);
          localStorage.setItem('electronicsStoreLogo',res.Logo);
          this.currentStoreName.next(res.Name);
          this.currentStoreLogo.next(res.Logo);
        });
        this.router.navigateByUrl('/');
    }
    getUser(id:any,storeId:any) : Observable<UserResponse>{
        return this.http.get<UserResponse>(`${environment.baseUrl}user/${id}/${storeId}`);
    }
    getUserAddress(id:any,storeId:any) : Observable<UserAddress[]>{
        return this.http.get<UserAddress[]>(`${environment.baseUrl}address/AddressForUser?userId=${id}&storeId=${storeId}`);
    }
    setDefaultAddress(addressId:number){
        return this.http.post<UserAddress>(`${environment.baseUrl}address/SetDefaultAddress`,{Id :addressId,IsDefault :true});
    }
    addNewAddress(address:INewAddress){
        return this.http.post<UserAddress>(`${environment.baseUrl}address/NewAddress`,address);
    }
    updateUser(id:any,storeId:number,name:string,password:string,gender:number,phone:string){
      return this.http.put<UserResponse>(`${environment.baseUrl}user/update`,{id,StoreId:storeId,Name : name,Password:password,Gender:gender,Phone1:phone});
    }
}
