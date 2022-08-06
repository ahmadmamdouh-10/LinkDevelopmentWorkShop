import { UserResponse } from './../models/userResponse.model';
import { UserService } from 'src/app/services/user.service';
import { IProduct } from './../models/IProduct.model';
import { ProductService } from './../services/product.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Observable } from 'rxjs';
import { IProductDetails } from '../models/IProduct.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-popular-products',
  templateUrl: './popular-products.component.html',
  styleUrls: ['./popular-products.component.css']
})
export class PopularProductsComponent implements OnInit {

  products$: Observable<IProduct[]> | undefined;
  product!: IProductDetails;
  storeId: number =1;
  lang!: string;
  user:any;

  constructor(private prodService: ProductService,
    private userService: UserService,
    private router: Router) { }

  ngOnInit(): void {
    if (this.userService.isLoggedIn()) {
      this.user = this.userService.currentUser.value;
      this.storeId = this.user.Store.Id;
    }
    else
      this.storeId = 1;
    this.products$ = this.prodService.latestProducts(this.storeId);
  }

  // getProductById(productId:any, template: TemplateRef<any>) {
  //   let userId = this.userService.isLoggedIn() ? this.user.Id : -1;
  //   this.prodService.getById(productId, this.storeId, userId)
  //     .subscribe(prod => {
  //       this.product = prod;
  //       console.log(this.product);
  //     });
  // }

  showProducts() {
    if(this.products$ != null)
    this.products$
    .subscribe((res: any) => {
      console.log(res)
      this.router.navigateByUrl('products/' + res[0].Category);
    })
  }
}
