import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import 'rxjs';


@Injectable()
export class travelservice {

  constructor(private http: HttpClient, private router: Router) {
  }
  //travelchoice(username: string, password: string, name: string, selectednudge: TravelTypes): void {
  //  console.log(name);
  //  //http get request here
  //}
}
