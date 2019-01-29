import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { userservice } from './userservice';

@Component({
  templateUrl: './signup.html',
  providers: [userservice],
})
export class SignupComponent {

  constructor(private userService: userservice) { }

  public validate() {
    this.userService.callGet();
  }
}
