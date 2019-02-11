import { Injectable } from '@angular/core';
import { HttpParams, HttpClient } from '@angular/common/http';
import 'rxjs';
import { Subscription } from '../types/Subscription';

@Injectable()
export class subscriptionservice {

  constructor(private http: HttpClient) {
  }

  addSubscription(username: string, subscription: Subscription): void {

    this.http.post('http://localhost:5000/Api/PushNotification/Subscribe', subscription , { responseType: 'text' }).pipe()
      .subscribe(result => {    }, error => console.error(error));
  }
}

