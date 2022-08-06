import { UserService } from 'src/app/services/user.service';
import { OfferService } from './../services/offer.service';
import { Component, OnInit } from '@angular/core';
import { Offer } from '../models/offer.model';
declare var $: any;

@Component({
  selector: 'app-cover',
  templateUrl: './app-cover.component.html',
  styleUrls: ['./app-cover.component.css']
})
export class AppCoverComponent implements OnInit {

  offers: Offer[] | undefined;
  storeId: number;
  lang: string = 'en';

  constructor(private offerService: OfferService,
    private userService: UserService) {
    if (this.userService.isLoggedIn())
      this.storeId = this.userService.currentUser.value.Store.Id;
      else
      this.storeId = 0;
    this.offerService.offers(this.storeId, this.lang)
      .subscribe(offers => {
        this.offers = offers;
        $(function () {
          $('.ver1 .tp-banner').revolution({
            delay: 9000,
            startwidth: 1860,
            startheight: 810,
            hideThumbs: 10,
            fullWidth: "on",
            forceFullWidth: "on"
          });
        });
      });
  }

  ngOnInit(): void {

  }

}
