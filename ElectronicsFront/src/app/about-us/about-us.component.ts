import { UserService } from 'src/app/services/user.service';
import { AppService } from './../services/app.service';
import { IFooter } from './../models/IFooter.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-about-us',
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.css']
})
export class AboutUsComponent implements OnInit {

  aboutUs: IFooter | undefined;
  storeId:number;

  constructor(private appService:AppService,
              private userService:UserService) {
                this.storeId = 0;
              }

  ngOnInit() {
    if(this.userService.isLoggedIn())
    this.storeId = this.userService.currentUser.value.Store.Id;

    this.appService.getAbout(this.storeId)
      .subscribe((res: any) => {
        this.aboutUs = res;
    });
  }

}
