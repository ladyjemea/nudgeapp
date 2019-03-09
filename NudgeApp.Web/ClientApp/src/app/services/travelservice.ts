/// <reference path="../../../node_modules/@types/googlemaps/index.d.ts"/>

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import 'rxjs';
import { Promise } from 'es6-promise';
import { MapsAPILoader } from '@agm/core';
import { TripDto } from '../types/TripDto';
import { Time } from '@angular/common';


@Injectable()
export class travelservice {

  constructor(private http: HttpClient, private mapsAPILoader: MapsAPILoader) {
  }

  public GetTrip(to: string, date: Date, mode: google.maps.TravelMode, callback: ICallback): void {
    this.mapsAPILoader.load().then(() => {
      navigator.geolocation.getCurrentPosition((position) => {
        var geocoder = new google.maps.Geocoder;

        //var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
        var latlng = new google.maps.LatLng(69.68084373889975, 18.976014381857112);
        geocoder.geocode({ 'address': to }, function (results, status) {

          var distanceMatrixService = new google.maps.DistanceMatrixService();
          distanceMatrixService.getDistanceMatrix({
            origins: [latlng],
            destinations: [results[0].geometry.location],
            travelMode: mode
          }, (result, status) => {

              var res = <TripDto>{}
              res.Duration = <Time>{};
              res.DistanceString = result.rows[0].elements[0].distance.text;
              res.Duration.minutes = result.rows[0].elements[0].duration.value;
              callback(res);
          });
        });
      });
    });
  }
}

export interface ICallback {
  (result: TripDto): void;
}
