import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { AuthService, FacebookLoginProvider, GoogleLoginProvider } from 'angular-6-social-login';
import { Router } from '@angular/router';

@Component({
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  providers: [AuthenticationService, AuthService],
})
export class LoginComponent {

  constructor(private authenticationService: AuthenticationService, private socialAuthService: AuthService, private router: Router) { }

  public loginUser(form: NgForm) {
    this.authenticationService.login(form.value.username, form.value.password).subscribe((result) =>
      this.router.navigateByUrl('mainaccess'));
  }
  

  public GoogleSignIn() {
    this.socialAuthService.signIn(GoogleLoginProvider.PROVIDER_ID).then(
      (userData) => {
        this.authenticationService.GoogleLogin(userData.id, userData.idToken, userData.email).subscribe((result) =>
          this.router.navigateByUrl('mainaccess'));
      });
  }
}
