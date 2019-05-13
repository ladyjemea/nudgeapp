/// <reference path="../../../../node_modules/@types/googlemaps/index.d.ts"/>

import { Component, Input, OnInit, NgZone } from '@angular/core';
import 'rxjs';
import { SwPush } from '@angular/service-worker';
import { SubscriptionService } from '../../services/SubscriptionService'
import { Subscription } from '../../types/Subscription'
import { MapsAPILoader } from '@agm/core';
import { Router } from '@angular/router';
import { TravelVariant } from '../../types/TravelVariant';
import { CalendarService, UserEvent } from 'src/app/services/CalendarService';
import { EventService } from 'src/app/services/EventService';
import { NudgeCoordinates } from '../../types/TripDto';


@Component({
  selector: 'search',
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [SwPush, SubscriptionService, CalendarService, EventService],
})

export class MainaccessComponent implements OnInit {
  [x: string]: any;

  readonly VAPID_PUBLIC_KEY = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";

  lat: number;
  lng: number;

  travelVariants: TravelVariant[];

  onChooseLocation($event) {
    this.mapsAPILoader.load().then(() => {
      var geocoder = new google.maps.Geocoder;
      var latlng = new google.maps.LatLng($event.coords.lat, $event.coords.lng);
      geocoder.geocode({ 'location': latlng }, function (results, status) {
      });
    });

  }

  constructor(
    private router: Router, private swPush: SwPush,
    private subscriptionservice: SubscriptionService, private mapsAPILoader: MapsAPILoader,
    private calendarService: CalendarService, private ngZone: NgZone, private eventService: EventService) {


    this.subscribeToNotifications();

    navigator.geolocation.getCurrentPosition((position) => {
      this.lat = position.coords.latitude;
      this.lng = position.coords.longitude;

      this.calendarService.GetEvents((events) => {
        console.log(events);
        events.forEach((event) => {
          let coordinates: NudgeCoordinates = <NudgeCoordinates>{};
          coordinates.Latitude = this.lat;
          coordinates.Longitude = this.lng;

          this.eventService.sendEvent(event, coordinates);
        });
      })
    }); 
  }

  ngOnInit() { }

  submitSearch(event, formData) {
   
    let query = formData.value["destination"];
    if (query) {
      this.router.navigate(['search', { destination: query }]);
    }
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
