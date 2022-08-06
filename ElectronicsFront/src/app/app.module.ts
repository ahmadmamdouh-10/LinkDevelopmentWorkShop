import { environment } from './../environments/environment';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './user/register/register.component';
import { LoginComponent } from './user/login/login.component';
import { NavbarComponent } from './navbar/navbar.component';
import { HomeComponent } from './home/home.component';
import { CategoriesComponent } from './categories/categories.component';
import { ProductsComponent } from './products/products.component';
import { AuthComponent } from './auth/auth.component';
import { FooterComponent } from './footer/footer.component';
import { AppCoverComponent } from './app-cover/app-cover.component';
import { PopularProductsComponent } from './popular-products/popular-products.component';
import { AboutUsComponent } from './about-us/about-us.component';
import { MyProfileComponent } from './my-profile/my-profile.component';
import { ContactComponent } from './contact/contact.component';
import { OffersComponent } from './offers/offers.component';


@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    NavbarComponent,
    HomeComponent,
    CategoriesComponent,
    ProductsComponent,
    AuthComponent,
    FooterComponent,
    AppCoverComponent,
    PopularProductsComponent,
    AboutUsComponent,
    MyProfileComponent,
    ContactComponent,
    OffersComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
