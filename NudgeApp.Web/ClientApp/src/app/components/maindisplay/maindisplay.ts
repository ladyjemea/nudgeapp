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

  @Input() message: string;

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
      this.travelService.GetTrip('Norway, Tromsø, Luleåvegen 19', date, google.maps.TravelMode.BICYCLING,
        (result) => {
          this.bicycling = result.durationString;
          console.log(result)
          console.log(this.bicycling);

          ref.detectChanges();
        });

      this.travelService.GetTrip('Norway, Tromsø, Luleåvegen 19', date, google.maps.TravelMode.TRANSIT,
        (result) => {
          this.bus = result.durationString;
          console.log(result)
          console.log(this.bus);

          ref.detectChanges();
        });

      this.travelService.GetTrip('Norway, Tromsø, Luleåvegen 19', date, google.maps.TravelMode.DRIVING,
        (result) => {
          this.distance = result.distanceString;
          console.log(result)
          console.log(this.distance);

          ref.detectChanges();
        });

    });

  }
  public pickTravelMode(form: NgForm) {

    //this.authenticationService.checkpassword(form.value.username, form.value.password);
  }
}
