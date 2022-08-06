import { IStore } from './../models/IStore.model';
import { environment } from './../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class GlobalService {
  constructor(private http: HttpClient) {}

  stores() {
    return this.http.get<IStore[]>(`${environment.baseUrl}store/stores`);
  }
  userStores(userId: any) {
    return this.http.get<IStore[]>(
      `${environment.baseUrl}store/GetUserStores?userId=${userId}`
    );
  }

  store() {
    return this.http.get<IStore>(`${environment.baseUrl}store/RandomStore`);
  }

}
