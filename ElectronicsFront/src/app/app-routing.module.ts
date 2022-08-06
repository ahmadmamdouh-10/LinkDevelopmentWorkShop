import { OffersComponent } from './offers/offers.component';
import { AdminService } from './resolvers/admin.service';
import { ContactComponent } from './contact/contact.component';
import { MyProfileComponent } from './my-profile/my-profile.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { PopularProductsComponent } from './popular-products/popular-products.component';
import { AboutResolver } from './resolvers/about-resolver.service';
import { FooterComponent } from './footer/footer.component';
import { AuthComponent } from './auth/auth.component';
import { ProductsComponent } from './products/products.component';
import { CategoriesComponent } from './categories/categories.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {component: HomeComponent , path:''},
  {component: HomeComponent , path:'home'},
  {component: AuthComponent , path:'auth-login'},
  {component: CategoriesComponent , path:'categories'},
  {component: ProductsComponent , path:'products/:category'},
  {component: PopularProductsComponent , path:'popular-products'},
  {component: LoginComponent , path:'products'},
  {component: AboutUsComponent , path:'about-us'},
  {component: MyProfileComponent, path:'my-profile',canActivate:[AdminService]},
  {component: OffersComponent, path:'offers'},
  {component: ContactComponent, path:'contact'},
  {path :'**' , redirectTo :'/' ,pathMatch : 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
