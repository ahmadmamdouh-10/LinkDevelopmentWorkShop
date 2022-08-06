import { UserResponse } from './../../models/userResponse.model';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(private userService:UserService,
    private fb:FormBuilder,
    public loginFormGroup : FormGroup,
    private router:Router) { }

    // loginFormGroup : FormGroup;
    lang:string= 'en';
    ngOnInit(): void {
      this.loginFormGroup = this.fb.group({
        'email': ['',[Validators.required,Validators.email]],
        'password' :['',Validators.required],
      });

    }

    login(){
      if(this.loginFormGroup.invalid)
        return;

       var loginModel = Object.assign({},this.loginFormGroup.value,{deviceToken:'browser'});
       this.userService.login(loginModel)
        .subscribe((res:UserResponse)=>{
                //logic for user in only one store
                if(res.User != null){
                  localStorage.removeItem('electronicsStoreId');
                  localStorage.setItem('electronicsUser',JSON.stringify(res.User));
                  localStorage.setItem('electronicsStoreName',res.User.Store.Name);
                  this.userService.currentUser.next(res.User);
                  this.userService.currentStoreName.next(res.User.Store.Name);
                  this.router.navigate(['/']);
                }
                //logic for multi stores
                else{
                    localStorage.setItem('stores',JSON.stringify(res.Stores));
                    localStorage.setItem('userId',res.UserId.toString());
                    this.router.navigateByUrl('/auth-login');
                }

        },err=>{
          console.log(err);
        }) ;
    }

}
