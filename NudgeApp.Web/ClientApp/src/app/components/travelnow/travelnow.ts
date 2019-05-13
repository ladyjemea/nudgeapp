import { Component, ChangeDetectorRef, Input, OnInit, OnDestroy } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { TravelService } from '../../services/TravelService';
import { WeatherService } from '../../services/WeatherService';
import { MapsAPILoader } from '@agm/core';
import { TripDto, TransportationType } from '../../types/TripDto';
import { ActivatedRoute } from '@angular/router';
import { query } from '@angular/core/src/render3/query';
import { ForecastDto } from '../../types/ForecastDto';
import { NudgeService } from '../../services/NudgeService';
import { NudgeResult } from '../../types/Nudge';


@Component({
  templateUrl: './travelnow.html',
  styleUrls: ['./travelnow.css'],
  providers: [TravelService, WeatherService, NudgeService],
})
export class TravelNowComponent implements OnInit, OnDestroy {
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

  get transportationTypes() { return TransportationType; }

  constructor(private travelService: TravelService, private mapsAPILoader: MapsAPILoader, private ref: ChangeDetectorRef, private route: ActivatedRoute, private weatherService: WeatherService, private nudgeService: NudgeService) {
    this.weatherService.GetCurrentForecast().subscribe((forecast) => {
      this.temperature = String(forecast.temperature);
      this.realfeelTemperature = String(forecast.realFeelTemperature);
      this.travelForecast = forecast;
    });
  }

  ngOnInit() {
    this.routeSub = this.route.params.subscribe(params => {
      this.query = params['destination'] + " , Tromso, Norway";

      this.mapsAPILoader.load().then(() => {

        var date = new Date(Date.now());
        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.WALKING,
          (result) => {
            this.walking = result.durationString;
            this.distance = result.distanceString;

            this.ref.detectChanges();

            this.walkingTrip = result;
            this.walkingTrip.transportationType = TransportationType.Walk;
          });

        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.BICYCLING,
          (result) => {
            this.bicycling = result.durationString;

            this.ref.detectChanges();

            this.bikeTrip = result;
            this.bikeTrip.transportationType = TransportationType.Bike;
          });
        this.travelService.GetTrip(this.query, date, google.maps.TravelMode.TRANSIT,
          (result) => {
            this.bus = result.durationString;

            this.ref.detectChanges();

            this.busTrip = result;
            this.busTrip.transportationType = TransportationType.Bus;
          });

      });
    })
  }

  ngOnDestroy() {
    this.routeSub.unsubscribe()
  }

  Nudge(travelType: TransportationType) {

    var trip = this.getTrip(travelType);

    if (travelType === TransportationType.Car) {
      this.nudgeService.saveNudge(NudgeResult.Failed, this.travelForecast, trip);
      var tripLink = trip.link.substr(0, trip.link.indexOf("&travelmode"));
      window.location.href = tripLink;
    }
    else {
      this.nudgeService.saveNudge(NudgeResult.Successful, this.travelForecast, trip);
      window.location.href = trip.link;
    }
  }

  private getTrip(travelType: TransportationType): TripDto {
    switch (travelType) {
      case TransportationType.Walk:
        return this.walkingTrip;
      case TransportationType.Bike:
        return this.bikeTrip;
      case TransportationType.Bus:
        return this.busTrip;
      case TransportationType.Car:
        return this.walkingTrip;
    }
  }

  public pickTravelMode(form: NgForm) {

  }
  showmaps(event) {

  }

}
