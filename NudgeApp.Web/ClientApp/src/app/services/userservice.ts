import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { TravelTypes } from '../preferredtravel/traveltypes';

@Injectable()
export class userservice {

  constructor(private http: HttpClient, private router: Router) {
  }

  createuser(username: string, password: string, name: string, email: string, address: string, selectedTravelType: TravelTypes): void {
    console.log(name);

    this.http.get('http://localhost:5000/User/register?username=' + username + '&password=' + password + '&name=' + name + '&email=' + email + '&address=' + address + '&travelType=' + selectedTravelType, { responseType: 'text' }).subscribe(result => {
      console.log(result);
      this.router.navigateByUrl('mainaccess');
    }, error => console.error(error));
  }

  checkpassword(username: string, password: string): void {

    this.http.get('http://localhost:5000/User/authenticate?username=' + username + '&password=' + password).pipe(map(user => {
      // login successful if there's a jwt token in the respons
      console.log(user);
      console.log(user['id']);
      console.log(user['token']);
      if (user && user['token']) {
        console.log('got in!');
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user))
        this.router.navigateByUrl('mainaccess');
      }
    })).subscribe();

  }
}

