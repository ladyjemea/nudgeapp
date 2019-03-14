
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import 'rxjs';
import { ForecastDto, IForecastCallback } from '../types/ForecastDto';
import { Observable } from 'rxjs';


@Injectable()
export class WeatherService {

  constructor(private http: HttpClient) { }

  public GetForecast(when: Date, callback: IForecastCallback) {
    this.http.post('Weather/GetForecast', when).subscribe(result => callback(<ForecastDto>result));
  }

  public GetCurrentForecast(): Observable<any> {
    return this.http.get('Weather/GetCurrentForecast');
  }
}
