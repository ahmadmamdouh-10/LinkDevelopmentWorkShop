import { GlobalService } from './../services/global.service';
import { Observable } from 'rxjs';
import { UserResponse } from './../models/userResponse.model';
import { IAddressModel, INewAddress, UserAddress } from './../models/IAddressModel';
import { FormGroup, FormBuilder, ValidatorFn, AbstractControl, ValidationErrors, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent implements OnInit {

  user:any;
  lang:string = 'en';
  addresses!: UserAddress[];

  constructor(private userService: UserService,
              private router: Router,
              private fb:FormBuilder,
              private gService:GlobalService,
              public profileFormGroup: FormGroup,
              public addressFormGroup:FormGroup,
              private globalService:GlobalService
              ) { }

  ngOnInit() {
    if (!this.userService.isLoggedIn)
      this.router.navigate(['/auth-login']);


    this.profileFormGroup = this.fb.group({
      'name':[this.user.Name],
      'email':[this.user.Email],
      'phone':[this.user.Phone],
      'gender':[this.user.Gender],
      'password':[],
      'confirmPassword':[]
    },{});

     this.userService.getUserAddress(this.user.Id,this.user.Store.id)
      .subscribe(res => this.addresses = res);

      this.addressFormGroup = this.fb.group({
        'street':['',Validators.required]
       });

  }

  logout(){
    this.userService.logout();
  }


  saveAddress(){
    let address:INewAddress = {
      Street : this.addressFormGroup.value.street,
      StoreId : this.user.Store.Id,
      UserId :this.user.Id,
      IsDefault:false,
      Id:0
    };

    this.userService.addNewAddress(address)
      .subscribe(res =>{
        this.addresses.push(res);
        let header = 'Address';
        let body = 'Address added successfully';
      },err=>{
        let header = 'Address';
        let body = 'Address failed to be addedd';
      });
  }

  toMyProfile(){
    this.router.navigate(['/my-profile'])
  }
}
