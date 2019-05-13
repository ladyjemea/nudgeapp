import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserEvent } from './CalendarService';
import { NudgeCoordinates } from '../types/TripDto';

@Injectable()
export class EventService {

  public static loggedIn: boolean = false;

  constructor(private http: HttpClient) { }

  sendEvent(event: UserEvent, userCoordinates: NudgeCoordinates) {
    var userEvent: UserEvent = <UserEvent>{};
    // @ts-ignore
    userEvent.start = new Date(event.start.dateTime);
    // @ts-ignore
    userEvent.end = new Date(event.end.dateTime);
    userEvent.name = event.name;
    userEvent.location = event.location;

    var parameters = {
      userEvent, userCoordinates
    };

    this.http.post('Analysis/AnalyseEvent', parameters).pipe()
      .subscribe(result => { }, error => console.error(error));
  }
}
