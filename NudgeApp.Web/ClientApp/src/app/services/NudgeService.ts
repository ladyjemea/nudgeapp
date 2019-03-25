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

  saveNudge(type: TransporationType, forecast: ForecastDto, trip: TripDto): void {
    var nudgeData: NudgeData = <NudgeData>{};
    nudgeData.transportationType = type;
    nudgeData.forecast = forecast;
    nudgeData.trip = trip;
    this.http.post('http://localhost:5000/Nudge/AddNudge', nudgeData, { responseType: 'text' }).subscribe(result => { });
  }
}

interface NudgeData {
  transportationType: TransporationType;
  forecast: ForecastDto;
  trip: TripDto;
}
