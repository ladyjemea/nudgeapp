/// <reference path="../../../node_modules/@types/googlemaps/index.d.ts"/>

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { MapsAPILoader } from '@agm/core';
import { TripDto, ITripCallback, TravelObject, NudgeCoordinates, TripSchedule } from '../types/TripDto';
import { Time } from '@angular/common';


@Injectable()
export class TravelService {

  constructor(private http: HttpClient, private mapsAPILoader: MapsAPILoader) {  }

  public GetTrip(to: string, date: Date, mode: google.maps.TravelMode, callback: ITripCallback): void {

    this.mapsAPILoader.load().then(() => {
      navigator.geolocation.getCurrentPosition((position) => {
        var geocoder = new google.maps.Geocoder;

        var latlng = new google.maps.LatLng(position.coords.latitude, position.coords.longitude);
       // var latlng = new google.maps.LatLng(69.68084373889975, 18.976014381857112);
        geocoder.geocode({ 'address': to }, (results, status) => {
          if (mode !== google.maps.TravelMode.TRANSIT) {
            var distanceMatrixService = new google.maps.DistanceMatrixService();
            distanceMatrixService.getDistanceMatrix({
              origins: [latlng],
              destinations: [results[0].geometry.location],
              travelMode: mode
            }, (result, status) => {
                console.log(result);
              var res = <TripDto>{}
              res.duration = <Time>{};
              res.distanceString = result.rows[0].elements[0].distance.text;
              res.duration.minutes = result.rows[0].elements[0].duration.value;
              res.durationString = result.rows[0].elements[0].duration.text;
              callback(res);
            });

          }
          else {
            var travelObject = <TravelObject>{};
            travelObject.From = <NudgeCoordinates>{};
            travelObject.From.Latitude = latlng.lat();
            travelObject.From.Longitude = latlng.lng();

            travelObject.To = <NudgeCoordinates>{};
            travelObject.To.Latitude = results[0].geometry.location.lat();
            travelObject.To.Longitude = results[0].geometry.location.lng();

            travelObject.When = date;
            travelObject.Schedule = TripSchedule.Departure;

            this.http.post('http://localhost:5000/Bus/GetTrip', travelObject, { responseType: 'json' }).pipe()
              .subscribe(result => { callback(<TripDto>result); }, error => console.error(error));
          }
        });
      });
    });
  }
}
