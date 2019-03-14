import { Component, ChangeDetectorRef, Input, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { travelservice } from '../../services/travelservice';
import { MapsAPILoader } from '@agm/core';
import { TripDto } from '../../types/TripDto';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { ActivatedRoute } from '@angular/router';
import { query } from '@angular/core/src/render3/query';


@Component({
  templateUrl: './maindisplay.html',
  styleUrls: ['./maindisplay.css'],
  providers: [travelservice],
})
export class MainDisplayComponent implements OnInit, OnDestroy {
  public walking: string = "";
  public bicycling: string = "";
  public bus: string = "";
  public distance: string = "";

  public query: string;
  public routeSub: any;
 
  

  constructor(private travelService: travelservice, private mapsAPILoader: MapsAPILoader, private ref: ChangeDetectorRef, private route: ActivatedRoute) {
    this.mapsAPILoader.load().then(() => {

      var date = new Date(Date.now());
      //query should take the search input (it does but gives an error in travelservice.ts (typeerror)
      this.travelService.GetTrip('query', date, google.maps.TravelMode.WALKING,
        (result) => {
          this.walking = result.durationString;
          this.distance = result.distanceString;

          ref.detectChanges();
        });
      this.travelService.GetTrip('Luleåvegen 19', date, google.maps.TravelMode.BICYCLING,
        (result) => {
          this.bicycling = result.durationString;

          ref.detectChanges();
        });

      this.travelService.GetTrip("luleåvegen 19", date, google.maps.TravelMode.TRANSIT,
        (result) => {
          this.bus = result.durationString;

          ref.detectChanges();
        });

    });
  }

    ngOnInit() {
      this.routeSub = this.route.params.subscribe(params => {
       // console.log(params)
        this.query = params['destination']
      })
    }

    ngOnDestroy() {
      this.routeSub.unsubscribe()
    }

  
  public pickTravelMode(form: NgForm) {

    //this.authenticationService.checkpassword(form.value.username, form.value.password);
  }
  
}
