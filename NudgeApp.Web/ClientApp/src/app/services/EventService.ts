import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { GoogleApiService } from 'ng-gapi';
import { Request } from '@angular/http';
import { Event } from 'src/app/services/CalendarService';

@Injectable()
export class EventService {

  public static loggedIn: boolean = false;

  constructor(private http: HttpClient) { }

  sendEvent(event: Event) {
    var sendEvent: Event = <Event>{};
    sendEvent.Start = new Date(event.Start.dateTime);
    sendEvent.End = new Date(event.End.dateTime);
    sendEvent.Name = event.Name;
    sendEvent.Location = event.Location;

    this.http.post('Analysis/GetEvent', sendEvent, { responseType: 'text' }).pipe()
      .subscribe(result => { }, error => console.error(error));
  }
}
