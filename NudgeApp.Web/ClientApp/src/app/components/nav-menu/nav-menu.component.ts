import { Component } from '@angular/core';
import { AuthenticationService } from '../../services/AuthenticationService';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  constructor(private authenticationService: AuthenticationService) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logOut() {
    this.authenticationService.logout();
  }

  public get LoggedIn() { return AuthenticationService.loggedIn; }
}
