import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { UserService } from '../../services/UserService';
import { AuthenticationService } from '../../services/AuthenticationService';
import { TravelTypes } from '../../types/TravelTypes';

@Component({
  templateUrl: './signup.html',
  styleUrls: ['./signup.css'],
  providers: [UserService, AuthenticationService]
})
export class SignupComponent {

  private selectedTravelType: TravelTypes;

  constructor(private userService: UserService, private authenticationService: AuthenticationService) {
    this.selectedTravelType = TravelTypes.Car;
  }

  public Selected(value: any) {
    console.log(value);
    this.selectedTravelType = value;
  }

  public registerUser(form: NgForm) {
    console.log(form.value);

    this.userService.createuser(form.value.username, form.value.password, form.value.name, form.value.email, form.value.address, this.selectedTravelType)
      .subscribe(() => { this.authenticationService.login(form.value.username, form.value.password); });
  }
}
