import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { Router } from '@angular/router';
import { ForecastDto } from '../types/ForecastDto';
import { TripDto, TransportationType } from '../types/TripDto';
import { NudgeResult } from '../types/Nudge';

@Injectable()
export class NudgeService {

  constructor(private http: HttpClient, private router: Router) {
  }

  saveNudge(nudgeResult: NudgeResult, forecast: ForecastDto, trip: TripDto): void {
    var nudgeData = <NudgeData>{};
    nudgeData.forecast = forecast;
    nudgeData.trip = trip;
    this.http.post('Nudge/AddNudge', nudgeData, { responseType: 'text' }).pipe().subscribe(result => { });
  }
}

interface NudgeData {
  nudgeResult: NudgeResult;
  forecast: ForecastDto;
  trip: TripDto;
}
