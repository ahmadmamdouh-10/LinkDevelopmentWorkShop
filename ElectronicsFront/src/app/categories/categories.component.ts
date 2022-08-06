import { UserService } from 'src/app/services/user.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CategoryService } from './../services/category.service';
import { Component, OnInit } from '@angular/core';
import { ICategory } from '../models/ICategory.model';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {

  constructor(private catService:CategoryService,
              private router:Router,
              public categories$:Observable<ICategory[]>,
              private userService:UserService) { }

  // categories$:Observable<ICategory[]>;
  lang:string = 'en';
  storeId:number = 0;

  ngOnInit(): void {
    if(this.userService.isLoggedIn())
      this.storeId = this.userService.currentUser.value.Store.Id;

    this.categories$  = this.catService.getAll(this.storeId);
  }

  getProductsByCategory(category:string){
    this.router.navigate(['/products',category]);
  }

}
