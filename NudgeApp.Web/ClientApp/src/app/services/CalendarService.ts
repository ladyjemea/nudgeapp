import { Injectable } from '@angular/core';
import { GoogleApiService } from 'ng-gapi';

@Injectable()
export class CalendarService {
  constructor(private gapiService: GoogleApiService) { }

  public GetEvents(callback: IEventsCallback) {
    this.gapiService.onLoad().subscribe(() => {
      gapi.load('client', () => {
        gapi.client.load('calendar', "v3", () => {
          // @ts-ignore
          gapi.client.calendar.events.list({ 'calendarId': 'primary' })
            .execute((resp) => {
              var events: Array<UserEvent> = [];
              resp.result.items.forEach((event) => {
                var ev = <UserEvent>{};
                ev.start = event.start;
                ev.end = event.end;
                ev.location = event.location;
                ev.name = event.summary;
                events.push(ev);
              });
              
              events = events.filter(event => {
                // @ts-ignore
                var d = new Date(event.start.dateTime);
                return d > new Date(Date.now());
              })

              events = events.sort((event1, event2) => {
                // @ts-ignore
                var d1 = new Date(event1.start.dateTime);
                // @ts-ignore
                var d2 = new Date(event2.start.dateTime);
                if (d1 > d2)
                  return 1;
                else return -1;
              });

              callback(events);
            });
        });
      });
    });
  }
}

export interface UserEvent {
  location: string,
  start: Date,
  end: Date,
  name: string
}

export interface IEventsCallback {
  (results: Array<UserEvent>): void;
}
