import { AppService } from './../services/app.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css']
})
export class ContactComponent implements OnInit {

  lang:string='en';
  // contactFormGroup:FormGroup;

  constructor(private userService: UserService,
              public contactFormGroup:FormGroup,
              private appService:AppService,
              private fb :FormBuilder) { }

  ngOnInit() {

    this.contactFormGroup = this.fb.group({
      'name' :['',Validators.required],
      'email':['',[Validators.required,Validators.email]],
      'message':['',Validators.required]
    });

  }
  contact() {
    if(this.contactFormGroup.invalid)
      return;

    let storeId = localStorage.getItem('electronicsStoreId') || this.userService.currentUser.value.Store.Id;
    let obj:{name:any,email:any,message:any} = this.contactFormGroup.value
    this.appService.contact(obj.name,obj.email,obj.message,storeId)
      .subscribe(res=>{
      this.contactFormGroup.reset(this.contactFormGroup);
        let header = 'Contact';
        let body = 'Message send successfully';
      },err=>{
        let header = 'Contact';
        let body = 'Failed to send message';
      });
  }

}
