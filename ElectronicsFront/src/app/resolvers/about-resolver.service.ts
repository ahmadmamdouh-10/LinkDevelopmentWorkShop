import { HttpClient } from '@angular/common/http';
import { IFooter } from './../models/IFooter.model';
import { Injectable } from '@angular/core';
import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
    providedIn : 'root'
})

export class AboutResolver implements Resolve<IFooter>{

    constructor(private http:HttpClient) {

    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
         Observable<IFooter> {
             let storeId = 1;

        return this.http.get<IFooter>(`${environment.baseUrl}app/aboutus?storeId=${storeId}`);

    }

}
