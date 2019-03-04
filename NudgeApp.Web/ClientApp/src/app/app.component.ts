import { Component } from '@angular/core';
import { AuthenticationService } from './services/AuthenticationService';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [AuthenticationService]
})
export class AppComponent {
  title = 'app';

  constructor(private authenticationService: AuthenticationService) {
    this.authenticationService.checkToken();
  }
}
