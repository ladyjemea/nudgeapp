import { Component, NgZone } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { Router } from '@angular/router';

@Component({
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  providers: [AuthenticationService]
})
export class LoginComponent {

  constructor(private authenticationService: AuthenticationService, private router: Router, private ngZone: NgZone) { }

  public loginUser(form: NgForm) {
    this.authenticationService.login(form.value.email, form.value.password).subscribe((result) =>
      this.router.navigateByUrl('mainaccess'));
  }
  
  public GoogleSignIn() {
    this.authenticationService.GoogleLogin(() => this.ngZone.run(() => this.router.navigateByUrl('mainaccess')).then());
  }
}
