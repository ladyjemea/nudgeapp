import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { userservice } from './userservice';
import { TravelTypes } from '../preferredtravel/traveltypes';

@Component({
  templateUrl: './signup.html',
  styleUrls: ['./signup.css'],
  providers: [userservice],
})
export class SignupComponent {

  private selectedTravelType: TravelTypes;

  constructor(private userService: userservice) {
    this.selectedTravelType = TravelTypes.Car;
  }

  public Selected(value: any) {
    console.log(value);
    this.selectedTravelType = value;
  }

  public registerUser(form: NgForm) {
    console.log(form.value);

    this.userService.createuser(form.value.username, form.value.password, form.value.name, form.value.email, form.value.address);
  }
}
