import { Component, NgZone } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { AuthenticationService } from '../../services/AuthenticationService';
import { Router } from '@angular/router';
import { CalendarService } from '../../services/CalendarService';

@Component({
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  providers: [AuthenticationService, CalendarService]
})
export class LoginComponent {

  constructor(private authenticationService: AuthenticationService, private calendarService: CalendarService,
    private router: Router, private ngZone: NgZone) { }

  public loginUser(form: NgForm) {
    this.authenticationService.login(form.value.username, form.value.password).subscribe((result) =>
      this.router.navigateByUrl('mainaccess'));
    
  }
  
  public GoogleSignIn() {
    this.authenticationService.GoogleLogin(() => this.ngZone.run(() => this.router.navigateByUrl('mainaccess')).then());
  }
}
