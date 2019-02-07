import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
//import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { Router } from '@angular/router';
import { TravelTypes } from '../preferredtravel/traveltypes';

@Injectable()
export class userservice {

  constructor(private http: HttpClient, private router: Router) {
  }

  createuser(username: string, password: string, name: string, email: string, address: string, selectedTravelType: TravelTypes): void {
    console.log(name);

    this.http.get('http://localhost:5000/Api/User/createUser?username=' + username + '&password=' + password + '&name=' + name + '&email=' + email + '&address=' + address + '&travelType=' + selectedTravelType, { responseType: 'text' }).subscribe(result => {
      console.log(result);
      this.router.navigateByUrl('mainaccess');
    }, error => console.error(error));
  }

  checkpassword(username: string, password: string): void {
    console.log(username);
    // console.log(password);

    this.http.get('http://localhost:5000/Api/User/checkPassword?username=' + username + '&password=' + password, { responseType: 'text' }).subscribe(result => {
      console.log(result);
      this.router.navigateByUrl('mainaccess');
    }, error => console.error(error));

  }
}

