import { Component, ChangeDetectorRef, Input, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { TravelService } from '../../services/TravelService';
import { WeatherService } from '../../services/WeatherService';
import { MapsAPILoader } from '@agm/core';
import { TripDto, TransporationType } from '../../types/TripDto';
import { ActivatedRoute } from '@angular/router';
import { query } from '@angular/core/src/render3/query';
import { ForecastDto } from '../../types/ForecastDto';
import { NudgeService } from '../../services/NudgeService';


@Component({
  templateUrl: './maindisplay.html',
  styleUrls: ['./maindisplay.css'],
  providers: [TravelService, WeatherService, NudgeService],
})
export class MainDisplayComponent implements OnInit, OnDestroy {
  public walking: string = "";
  public bicycling: string = "";
  public bus: string = "";
  public distance: string = "";
  public temperature: string = "";
  public realfeelTemperature: string = "";

  private travelForecast: ForecastDto;
  private walkingTrip: TripDto;
  private bikeTrip: TripDto;
  private busTrip: TripDto;

  public query: string;
  public routeSub: any;

  get transportationTypes() { return TransporationType; }

  constructor(private travelService: TravelService, private mapsAPILoader: MapsAPILoader, private ref: ChangeDetectorRef, private route: ActivatedRoute, private weatherService: WeatherService, private nudgeService: NudgeService) {
    // this.weatherService.GetCurrentForecast().subscribe((forecast) => { this.temperature = String(forecast.temperature); this.travelForecast = forecast; });
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

            this.walkingTrip = result;
            this.walkingTrip.mode = TransporationType.Walk;
          });

        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.BICYCLING,
          (result) => {
            this.bicycling = result.durationString;

            this.ref.detectChanges();

            this.bikeTrip = result;
            this.bikeTrip.mode = TransporationType.Walk;
          });
        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.TRANSIT,
          (result) => {
            this.bus = result.durationString;

            this.ref.detectChanges();

            this.busTrip = result;
            this.busTrip.mode = TransporationType.Walk;
          });

      });
    })
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe()
  }

  Nudge(travelType: TransporationType) {

    var trip = this.getTrip(travelType);
    
    this.nudgeService.saveNudge(travelType, this.travelForecast, trip);

    window.location.href = trip.link;
  }

  private getTrip(travelType: TransporationType): TripDto {
    switch (travelType) {
      case TransporationType.Walk:
        return this.walkingTrip;
      case TransporationType.Bike:
        return this.bikeTrip;
      case TransporationType.Bus:
        return this.busTrip;
      case TransporationType.Car:
        return this.walkingTrip;
    }
  }

  public pickTravelMode(form: NgForm) {

    //this.authenticationService.checkpassword(form.value.username, form.value.password);
  }
  showmaps(event) {
   
  }

}
