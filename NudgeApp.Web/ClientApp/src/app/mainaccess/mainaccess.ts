/// <reference path="../../../node_modules/@types/googlemaps/index.d.ts"/>

import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { SwPush } from '@angular/service-worker';
import { subscriptionservice } from '../services/SubscriptionService'
import { Subscription } from '../types/Subscription'
import { MapsAPILoader } from '@agm/core';
import { userservice } from '../services/userservice';


@Component({
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [userservice, SwPush, subscriptionservice],
})

export class MainaccessComponent {

  readonly VAPID_PUBLIC_KEY = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";

  lat: number = 51.678418;
  lng: number = 7.809007;


  constructor(
    private swPush: SwPush,
    private userService: userservice,
    private subscriptionservice: subscriptionservice, private mapsAPILoader: MapsAPILoader) {
    this.subscribeToNotifications();

    navigator.geolocation.getCurrentPosition((position) => {
      console.log(position);
      this.lat = position.coords.latitude;
      this.lng = position.coords.longitude;


    })
  }
  test() {
    //gets the current location of the user
    this.mapsAPILoader.load().then(() => {
      var geocoder = new google.maps.Geocoder;
      var latlng = new google.maps.LatLng(this.lat, this.lng);
      /*geocoder.geocode({ 'location': latlng }, function (results, status) {
        console.log(results);
      });*/
      var address = 'Norway, Tromsø, Luleåvegen 19'; // should get the destination from the input field "enter destination"
      geocoder.geocode({ 'address': address }, function (results, status) {
        console.log(results[0].geometry.location);
        //const distance = google.maps.geometry.spherical.computeDistanceBetween(latlng, results[0].geometry.location);
        //console.log(distance);
        var request = {
          origin: latlng,
          destination: results[0].geometry.location,
          travelMode: google.maps.TravelMode.WALKING
     
        };

        /*var directionsService = new google.maps.DirectionsService();
        directionsService.route(request, (result, status) => {
          console.log('Travel: ');
          console.log(result);
        });*/

        var distanceMatrixService = new google.maps.DistanceMatrixService();
        distanceMatrixService.getDistanceMatrix({
          origins: [latlng],
          destinations: [results[0].geometry.location],
          travelMode: google.maps.TravelMode.WALKING
        }, (result, status) => { console.log(result.rows[0].elements[0].duration); });
      });
    });
  }



  public userLocation(form: NgForm) {


  }
  private subscribeToNotifications() {

    this.swPush.requestSubscription({
      serverPublicKey: this.VAPID_PUBLIC_KEY
    })
      .then(sub => {
        var jsonSub = sub.toJSON();
        var subscription = new Subscription();
        subscription.auth = jsonSub["keys"]["auth"];
        subscription.p256dh = jsonSub["keys"]["p256dh"];
        subscription.endpoint = sub["endpoint"];
        console.log(subscription);
        this.subscriptionservice.addSubscription("lae", subscription);
      })
      .catch(err => console.error("Could not subscribe to notifications", err));
  }

}
