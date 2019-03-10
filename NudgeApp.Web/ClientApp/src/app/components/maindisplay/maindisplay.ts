import { Component, ChangeDetectorRef } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { travelservice } from '../../services/travelservice';
import { MapsAPILoader } from '@agm/core';
import { TripDto } from '../../types/TripDto';

@Component({
  templateUrl: './maindisplay.html',
  styleUrls: ['./maindisplay.css'],
  providers: [travelservice],
})
export class MainDisplayComponent {
  public walking: string = "";

  constructor(private travelService: travelservice, private mapsAPILoader: MapsAPILoader, private ref: ChangeDetectorRef) {
    this.mapsAPILoader.load().then(() => {

      var date = new Date(Date.now());
      this.travelService.GetTrip('Norway, Tromsø, Luleåvegen 19', date, google.maps.TravelMode.WALKING,
        (result) => {
          this.walking = result.durationString;
          console.log(result)
          console.log(this.walking);

          ref.detectChanges();
        });
    });

  }
  public pickTravelMode(form: NgForm) {

    //this.authenticationService.checkpassword(form.value.username, form.value.password);
  }
}
