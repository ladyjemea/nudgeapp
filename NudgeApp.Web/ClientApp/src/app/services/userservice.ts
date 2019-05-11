import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Subscription, Observable } from 'rxjs';
import { TransportationType } from '../types/TripDto';

@Injectable()
export class UserService {

  constructor(private http: HttpClient, private router: Router) {
  }

  createuser(username: string, password: string, name: string, email: string, address: string, selectedTransporationType: TransportationType): Observable<string> {
    return this.http.get('User/register?username=' + username + '&password=' + password + '&name=' + name + '&email=' + email + '&address=' + address + '&travelType=' + selectedTransporationType, { responseType: 'text' });
  }
}
