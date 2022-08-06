import { UserService } from 'src/app/services/user.service';
import { AppService } from './../services/app.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  homeVideoUrl:string='';
  storeId: number=1;
  constructor(private appService:AppService ,
              private userService:UserService) { }

  ngOnInit(): void {
    if(this.userService.isLoggedIn()){
      let user = this.userService.currentUser.value;
       this.storeId= user.Store.Id
    }
    else
      this.storeId = Number.parseInt(localStorage.getItem('electonicsStoreId')|| '1');

    this.appService.homeVideo(this.storeId)
        .subscribe(res => this.homeVideoUrl = res.HomePageBackgroundVideoUrl)

  }

}
