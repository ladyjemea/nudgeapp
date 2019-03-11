import { Component, ChangeDetectorRef, Input } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { travelservice } from '../../services/travelservice';
import { MapsAPILoader } from '@agm/core';
import { TripDto } from '../../types/TripDto';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';


@Component({
  templateUrl: './maindisplay.html',
  styleUrls: ['./maindisplay.css'],
  providers: [travelservice],
})
export class MainDisplayComponent {
  public walking: string = "";
  public bicycling: string = "";
  public bus: string = "";
  public distance: string = "";

  public _url: string = "/assets/data/streets.json";

  constructor(private travelService: travelservice, private mapsAPILoader: MapsAPILoader, private ref: ChangeDetectorRef) {
    this.mapsAPILoader.load().then(() => {

      var date = new Date(Date.now());
      this.travelService.GetTrip('Luleavegen 19', date, google.maps.TravelMode.WALKING,
        (result) => {
          this.walking = result.durationString;
          this.distance = result.distanceString;

          ref.detectChanges();
        });
      this.travelService.GetTrip(this._url, date, google.maps.TravelMode.BICYCLING,
        (result) => {
          this.bicycling = result.durationString;        

          ref.detectChanges();
        });

      this.travelService.GetTrip(this._url, date, google.maps.TravelMode.TRANSIT,
        (result) => {
          this.bus = result.durationString;

          ref.detectChanges();
        });

    });

  }
  public pickTravelMode(form: NgForm) {

    //this.authenticationService.checkpassword(form.value.username, form.value.password);
  }
}
