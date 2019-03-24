/// <reference path="../../../../node_modules/@types/googlemaps/index.d.ts"/>

import { Component, Input, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SwPush } from '@angular/service-worker';
import { SubscriptionService } from '../../services/SubscriptionService'
import { Subscription } from '../../types/Subscription'
import { MapsAPILoader } from '@agm/core';
import { UserService } from '../../services/UserService';
import { ActivatedRoute, Router } from '@angular/router';
import { TravelVariant } from '../../types/TravelVariant';
import { TravelService } from '../../services/TravelService';
import { query } from '@angular/animations';


@Component({
  selector: 'search',
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [SwPush, SubscriptionService],
})

export class MainaccessComponent implements OnInit {
    [x: string]: any;

  public _url: string = "/assets/data/streets.json";
  readonly VAPID_PUBLIC_KEY = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";

  lat: number;
  lng: number;

  travelVariants: TravelVariant[];

  onChooseLocation($event) {
    this.mapsAPILoader.load().then(() => {
      var geocoder = new google.maps.Geocoder;
      var latlng = new google.maps.LatLng($event.coords.lat, $event.coords.lng);
      geocoder.geocode({ 'location': latlng }, function (results, status) {
        console.log("map results");
        console.log(results);
      });
    });

  }
  //continue from here tomorrow. continue from video

  constructor(
    private router: Router, private swPush: SwPush,
    private subscriptionservice: SubscriptionService, private mapsAPILoader: MapsAPILoader) {

    this.subscribeToNotifications();

    navigator.geolocation.getCurrentPosition((position) => {
      this.lat = position.coords.latitude;
      this.lng = position.coords.longitude;
    }); 
  }

  ngOnInit() { }

  submitSearch(event, formData) {
    //console.log(event);
    //console.log(formData.value);
    let query = formData.value["destination"];
    if (query) {
      this.router.navigate(['search', { destination: query }]);
      //this.router.navigateByUrl('/maindisplay');
    //this.http.post("/assets/data/streets.json", {})
    }
  }

  public userLocation(form: NgForm) {
   
   // this.router.navigateByUrl('/maindisplay');
  

  }

  private subscribeToNotifications() {

    this.swPush.requestSubscription({
      serverPublicKey: this.VAPID_PUBLIC_KEY
    })
      .then(sub => {
        var jsonSub = sub.toJSON();
        var subscription = <Subscription>{};
        subscription.auth = jsonSub["keys"]["auth"];
        subscription.p256dh = jsonSub["keys"]["p256dh"];
        subscription.endpoint = sub["endpoint"];

        this.subscriptionservice.addSubscription(subscription);
      })
      .catch(err => console.error("Could not subscribe to notifications", err));
  }

}
