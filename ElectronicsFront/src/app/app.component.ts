import { GlobalService } from './services/global.service';
import { UserService } from './services/user.service';
import { Component, OnInit } from '@angular/core';

import { IStore } from './models/IStore.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'EelectricsStore';
  constructor(private userService : UserService,
    private gService:GlobalService) {
  }

  ngOnInit(): void {
    var user = localStorage.getItem('yummyUser');
    var lang = localStorage.getItem('yummyLang');
    var storeName = localStorage.getItem('yummyStoreName');
    var storeLogo = localStorage.getItem('yummyStoreLogo');

    if(!this.userService.isLoggedIn()){
      if(!localStorage.getItem('electronicsStoreId'))
        this.gService.store()
          .subscribe((res:IStore) =>{
            localStorage.setItem('electronicsStoreId',res.Id.toString());
            localStorage.setItem('electronicsStoreName',res.Name);
            localStorage.setItem('electronicsStoreLogo',res.Logo);
            this.userService.currentStoreName.next(res.Name);
            this.userService.currentStoreLogo.next(res.Logo);
          });
    }

    if(user){
      this.userService.currentUser.next(JSON.parse(user));
    }
    if(storeName){
      this.userService.currentStoreName.next(storeName);
    }
    if(storeLogo){
      this.userService.currentStoreLogo.next(storeLogo);
    }

    if(lang){
       this.userService.currentLang.next(lang);
      document.documentElement.lang = lang;
      document.documentElement.dir = lang == 'ar' ? 'rtl' :'ltr' ;
    }
  }
}

