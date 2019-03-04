/// <reference path="../../../../node_modules/@types/googlemaps/index.d.ts"/>

import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { SwPush } from '@angular/service-worker';
import { subscriptionservice } from '../../services/SubscriptionService'
import { Subscription } from '../../types/Subscription'
import { MapsAPILoader } from '@agm/core';
import { TravelVariant } from '../../types/TravelVariant';


@Component({
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [SwPush, subscriptionservice],
})

export class MainaccessComponent {

  readonly VAPID_PUBLIC_KEY = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";

  lat: number;
  lng: number;

  travelVariants: TravelVariant[];

  constructor(private swPush: SwPush, private subscriptionservice: subscriptionservice, private mapsAPILoader: MapsAPILoader) {
    
    this.subscribeToNotifications();
    this.travelVariants = [];
    this.travelVariants.push(<TravelVariant>{ time: "some time", type: "walking" });
    this.travelVariants.push(<TravelVariant>{ time: "another time", type: "bike" });
    console.log(this.travelVariants);
    navigator.geolocation.getCurrentPosition((position) => {
      this.lat = position.coords.latitude;
      this.lng = position.coords.longitude;
    });
  }

  test() {
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
    this.mapsAPILoader.load().then(() => {
      var geocoder = new google.maps.Geocoder;
      var latlng = new google.maps.LatLng(this.lat, this.lng);

      var address = form.value.destination;
      geocoder.geocode({ 'address': address }, function (results, status) {
        var request = {
          origin: latlng,
          destination: results[0].geometry.location,
          travelMode: google.maps.TravelMode.WALKING
        };

        console.log('times:')
        var distanceMatrixService = new google.maps.DistanceMatrixService();
        distanceMatrixService.getDistanceMatrix({
          origins: [latlng],
          destinations: [results[0].geometry.location],
          travelMode: google.maps.TravelMode.WALKING
        }, (result, status) => { console.log('walk: '); console.log(result.rows[0].elements[0].duration); });

        distanceMatrixService.getDistanceMatrix({
          origins: [latlng],
          destinations: [results[0].geometry.location],
          travelMode: google.maps.TravelMode.BICYCLING
        }, (result, status) => { console.log('bike: '); console.log(result.rows[0].elements[0].duration); });

        distanceMatrixService.getDistanceMatrix({
          origins: [latlng],
          destinations: [results[0].geometry.location],
          travelMode: google.maps.TravelMode.TRANSIT
        }, (result, status) => { console.log('bus: '); console.log(result); });
      });
    });
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
        console.log(subscription);
        this.subscriptionservice.addSubscription("lae", subscription);
      })
      .catch(err => console.error("Could not subscribe to notifications", err));
  }

}
