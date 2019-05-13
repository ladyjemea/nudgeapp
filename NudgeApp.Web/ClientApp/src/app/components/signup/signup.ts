import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { UserService } from '../../services/UserService';
import { AuthenticationService } from '../../services/AuthenticationService';
import { TransportationType } from '../../types/TripDto';
import { Router } from '@angular/router';

@Component({
  templateUrl: './signup.html',
  styleUrls: ['./signup.css'],
  providers: [UserService, AuthenticationService]
})
export class SignupComponent {

  private selectedTravelType: TransportationType;

  constructor(private userService: UserService, private authenticationService: AuthenticationService, private router: Router) {
    this.selectedTravelType = TransportationType.Car;
  }

  public Selected(value: any) {
    this.selectedTravelType = value;
  }

  public registerUser(form: NgForm) {

    this.userService.createuser(form.value.password, form.value.name, form.value.email, form.value.address, this.selectedTravelType)
      .subscribe(() => {
        this.authenticationService.login(form.value.email, form.value.password).subscribe((result) =>
          this.router.navigateByUrl('mainaccess'));
      });

  }
}
