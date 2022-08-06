import { AppService } from './../services/app.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  loginBackground:string ='';
  lang:string = 'en';
  constructor(private appService:AppService) { }

  ngOnInit(): void {
    this.appService.loginBackground(1)
      .subscribe((res:any) =>  this.loginBackground = res.LogInPageBackgroundImageUrl);

  }

}
