import { UserService } from 'src/app/services/user.service';
import { CategoryService } from './../services/category.service';
import { ICategory } from './../models/ICategory.model';
import { IProduct, IProductDetails } from './../models/IProduct.model';
import { Observable } from 'rxjs';
import { ProductService } from './../services/product.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {

  constructor(private prodService:ProductService,
              private catService:CategoryService,
              private route:ActivatedRoute,
              private userService:UserService) { }

  lang:string='en';
  storeId:number=1;
  isSearch:boolean = false;
  searchCategory:string =  '';
  categories$:Observable<ICategory[]> | undefined;
  searchProducts$:Observable<IProduct[]> | undefined;
  products$:Observable<IProductDetails[]>| undefined;
  public product:IProductDetails | undefined;
  isChoosed:boolean = false;

  ngOnInit(): void {

    if(this.userService.isLoggedIn())
      this.storeId = this.userService.currentUser.value.Store.Id;
    else
      this.storeId = 1;

      if(this.searchCategory){
        this.searchCategory = this.route.snapshot.paramMap.get('category') || '';
      }

    if(this.searchCategory){
      this.getProductByCategory(this.searchCategory);
    }
    this.categories$ = this.catService.getAll(this.storeId);
  }

  getProductByCategory(category:any){
    this.isSearch = false;
    this.searchCategory = category;
    this.products$ = this.prodService.getByCategory(category,this.storeId);

  }

  getProductById(productId : any,template:TemplateRef<any>){
    let userId = this.userService.isLoggedIn() ? this.userService.currentUser.value.Id : -1;
     this.prodService.getById(productId,this.storeId,userId)
        .subscribe(prod => {this.product = prod});
  }

  result:string='';
  purchaseProduct(product:any, quantity:string){
    if(product != undefined){
      let qt = Number.parseInt(quantity);
      if(qt == 1){
        let total = product.Price *  product.Discount/100;

        this.isChoosed = true;

        this.result = 'your total price is ' + this.product;
        console.log(this.result);

      }else{
        let total = product.Price * qt * product.Discount/100;

        this.isChoosed = true;

        this.result = 'your total price is ' +total +' and you got a discount because you purchased more than one item.Thank you.';
        console.log(this.result);
      }

    }

  }

}
