import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import 'rxjs';
import { userservice } from './userservice';

@Component({
  templateUrl: './login.html',
  styleUrls: ['./login.css'],
  providers: [userservice],
})
export class LoginComponent {

  constructor(private userService: userservice) { }
  public loginUser(form: NgForm) {
    console.log(form.value);

    this.userService.checkpassword(form.value.username, form.value.passeord);
  }
}
