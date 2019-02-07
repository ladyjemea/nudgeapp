import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { userservice } from '../signup/userservice';


@Component({
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [userservice],
})

export class MainaccessComponent {

  constructor(private userService: userservice) { }
  public loginUser(form: NgForm) {

    this.userService
  }
 
}
