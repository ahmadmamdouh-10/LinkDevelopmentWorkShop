import { UserResponse } from './../../models/userResponse.model';
import { Observable } from 'rxjs';
import { GlobalService } from './../../services/global.service';
import { UserService } from './../../services/user.service';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IStore } from 'src/app/models/IStore.model';
import { Router } from '@angular/router';
import { RegisterModel } from 'src/app/models/RegisterModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private userService:UserService,
              private globalService:GlobalService,
              private fb:FormBuilder,
              public registerFormGroup : FormGroup,
              private router:Router) {
              }

  stores$:Observable<IStore[]> | undefined;

  isMap:boolean = false;
  lang:string = 'en';
  // registerFormGroup : FormGroup;


  ngOnInit(): void {
   //Get Current Positions
    navigator.geolocation.getCurrentPosition(pos=>{
      // this.lat = pos.coords.latitude;
      // this.lng = pos.coords.longitude;
    });

    this.registerFormGroup = this.fb.group({
      'name' : ['',Validators.required],
      'email': ['',[Validators.required,Validators.email]],
      'password' :['',Validators.required],
      'phone': ['',Validators.required],
      'code': ['',Validators.required],
      'gender' :['',Validators.required],
      'storeId' :[0,Validators.required],
      'address':this.fb.group({
        'street' :[null],
      })
    });

    this.stores$ = this.globalService.stores();
  }

  register(){
    if(this.registerFormGroup != null){
      if(this.registerFormGroup.invalid)
      return;

      var registerModel:RegisterModel =
      Object.assign({},this.registerFormGroup.value,{deviceToken:'browser'});
    registerModel.phone = registerModel.code + registerModel.phone;


    this.userService.register(registerModel)
    .subscribe((res : UserResponse) =>{

      //logic for user in only one store
        if(res.User != null){
          localStorage.removeItem('yummyStoreId');
          localStorage.setItem('yummyUser',JSON.stringify(res.User));
          localStorage.setItem('yummyStoreName',res.User.Store.Name);
          this.userService.currentStoreName.next(res.User.Store.Name);
          this.userService.currentUser.next(res.User);
          this.router.navigateByUrl('/');
        }
        //logic for multi stores
        else{
            localStorage.setItem('stores',JSON.stringify(res.Stores));
            localStorage.setItem('userId',res.UserId.toString());
            this.router.navigateByUrl('/select-store');
        }

    },err=>{
        console.log(err);
      });
    }
  }
}
