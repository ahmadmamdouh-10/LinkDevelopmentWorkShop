import { Offer } from './../models/offer.model';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { OfferService } from '../services/offer.service';
import { UserService } from '../services/user.service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-offers',
  templateUrl: './offers.component.html',
  styleUrls: ['./offers.component.css']
})
export class OffersComponent implements OnInit {

  constructor(public offerService: OfferService,
              private router: Router,
              public userService: UserService) { }
  offres$: Observable<Offer[]> | undefined;
  lang:string =  'en';


  ngOnInit() {
    let storeId = Number.parseInt(localStorage.getItem('electronicsStoreId') || '1');

    if(this.userService.isLoggedIn()){
      storeId = this.userService.currentUser.value.Store.Id;
    }
    this.offres$ =this.offerService.offers(storeId,this.lang);
  }


}
