import { Component, ChangeDetectorRef, Input, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { TravelService } from '../../services/TravelService';
import { WeatherService } from '../../services/WeatherService';
import { MapsAPILoader } from '@agm/core';
import { TripDto } from '../../types/TripDto';
import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { ActivatedRoute } from '@angular/router';
import { query } from '@angular/core/src/render3/query';


@Component({
  templateUrl: './maindisplay.html',
  styleUrls: ['./maindisplay.css'],
  providers: [TravelService, WeatherService],
})
export class MainDisplayComponent implements OnInit, OnDestroy {
  public walking: string = "";
  public bicycling: string = "";
  public bus: string = "";
  public distance: string = "";
  public temperature: string = "";
  public realfeelTemperature: string = "";

  public query: string;
  public routeSub: any;
  

  constructor(private travelService: TravelService, private mapsAPILoader: MapsAPILoader, private ref: ChangeDetectorRef, private route: ActivatedRoute, private weatherService: WeatherService) {
    this.weatherService.GetCurrentForecast().subscribe((forecast) => { this.temperature = String(forecast.temperature); });
    this.weatherService.GetCurrentForecast().subscribe((forecast) => { this.realfeelTemperature = String(forecast.realFeelTemperature); });
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.query = params['destination'];

      this.mapsAPILoader.load().then(() => {

        var date = new Date(Date.now());
        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.WALKING,
          (result) => {
            this.walking = result.durationString;
            this.distance = result.distanceString;

            this.ref.detectChanges();
          });

        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.BICYCLING,
          (result) => {
            this.bicycling = result.durationString;

            this.ref.detectChanges();
          });
        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.TRANSIT,
          (result) => {
            this.bus = result.durationString;

            this.ref.detectChanges();
          });

      });
    })
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe()
  }


  public pickTravelMode(form: NgForm) {

    //this.authenticationService.checkpassword(form.value.username, form.value.password);
  }

}
