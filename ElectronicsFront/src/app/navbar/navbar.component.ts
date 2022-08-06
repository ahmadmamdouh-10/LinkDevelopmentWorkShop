import { UserService } from './../services/user.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProductService } from '../services/product.service';
import { AppRoutingModule } from '../app-routing.module';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(public userService : UserService,
              public router:Router,
              private prodService:ProductService) { }
  // email:string;
  name:string = "";

  ngOnInit(): void {
    this.name = this.userService.currentUser.value?.Name;
  }

  isLoggedIn(){
    return this.userService.isLoggedIn();
  }

  logout(){
    this.userService.logout();
  }
}
