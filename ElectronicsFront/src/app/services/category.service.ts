import { ICategory } from './../models/ICategory.model';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";

@Injectable({
    providedIn : 'root'
})

export class CategoryService{
    
    constructor(private http:HttpClient) {
    }

    getAll(storeId:number){
        return this.http.get<ICategory[]>(`${environment.baseUrl}category?storeId=${storeId}`);
    }
}