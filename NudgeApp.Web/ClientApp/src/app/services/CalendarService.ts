import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import {  GoogleApiService } from 'ng-gapi';
import { Request } from '@angular/http';

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
              var events: Array<Event> = [];
              resp.result.items.forEach((event) => {
                var ev = <Event>{};
                ev.Start = event.start;
                ev.End = event.end;
                ev.Location = event.location;
                ev.Name = event.summary;
                events.push(ev);
              });

              callback(events);
            });
        });
      });
    });
  }
}

export interface Event {
  Location: string,
  Start: Date,
  End: Date,
  Name: string
}


export interface IEventsCallback {
  (results: Array<Event>): void;
}
