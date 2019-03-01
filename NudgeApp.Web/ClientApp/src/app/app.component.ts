import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  //title = 'app';
  logitude: 7.809007;
  latitude: 51.678418;

    onChooseLocation(event) {
    console.log(event);
  }
}
