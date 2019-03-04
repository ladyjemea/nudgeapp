import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { TravelTypes } from '../types/traveltypes';

@Injectable()
export class userservice {

  constructor(private http: HttpClient, private router: Router) {
  }

  createuser(username: string, password: string, name: string, email: string, address: string, selectedTravelType: TravelTypes): void {

    this.http.get('http://localhost:5000/User/register?username=' + username + '&password=' + password + '&name=' + name + '&email=' + email + '&address=' + address + '&travelType=' + selectedTravelType, { responseType: 'text' }).subscribe(result => {
      console.log(result);
      this.router.navigateByUrl('mainaccess');
    }, error => console.error(error));
  }
}

