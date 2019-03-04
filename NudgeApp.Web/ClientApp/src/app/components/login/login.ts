import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';

@Component({
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  providers: [AuthenticationService],
})
export class LoginComponent {

  constructor(private authenticationService: AuthenticationService) { }
  public loginUser(form: NgForm) {
    //console.log(form.value);

    this.authenticationService.checkpassword(form.value.username, form.value.password);
  }
}
