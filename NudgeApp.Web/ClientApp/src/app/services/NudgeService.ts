import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { Router } from '@angular/router';
import { ForecastDto } from '../types/ForecastDto';
import { TripDto, TransporationType } from '../types/TripDto';

@Injectable()
export class NudgeService {

  constructor(private http: HttpClient, private router: Router) {
  }

  saveNudge(forecast: ForecastDto, trip: TripDto): void {
    var nudgeData = <NudgeData>{};
    nudgeData.forecast = forecast;
    nudgeData.trip = trip;
    console.log(trip);
    this.http.post('Nudge/AddNudge', nudgeData, { responseType: 'text' }).pipe().subscribe(result => { });
  }
}

interface NudgeData {
  forecast: ForecastDto;
  trip: TripDto;
}
