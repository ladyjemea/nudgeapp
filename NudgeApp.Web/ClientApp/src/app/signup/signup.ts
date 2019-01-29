import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { userservice } from './userservice';

@Component({
  templateUrl: './signup.html',
  providers: [userservice],
})
export class SignupComponent {

  constructor(private userService: userservice) { }
  public registerUser(form: NgForm) {
    console.log(form.value.email);

    this.userService.callGet(form.value.username, form.value.password);
  }
}
