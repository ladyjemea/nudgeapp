import { Component, NgZone } from '@angular/core';
import { Options } from 'ng5-slider';
import { UserService } from '../../services/UserService';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { PreferencesDto } from 'src/app/types/PreferencesDto';
import { TransportationType } from 'src/app/types/TripDto';

@Component({
  selector: 'app-range-slider',
  templateUrl: './preferences.html',
  styleUrls: ['./preferences.css'],
  providers: [UserService]
})

export class PreferencesComponent {
  value: number = -50;
  highValue: number = 50;
  options: Options = {
    floor: -50,
    ceil: 50
  };
    snow: boolean;
    rain: boolean;
  selectedTravelType: TransportationType;

  constructor(private userService: UserService, private router: Router, private ngZone: NgZone) {
    this.userService.getPreferences().subscribe(result => {
      this.value = result.minTemperature;
      this.highValue = result.maxTemperature;
      this.selectedTravelType = result.transportationType;
      console.log(result );
      this.snow = result.snowyTrip;
      this.rain = result.rainyTrip;
    });
  }
  public Selected(value: any) {
    this.selectedTravelType = value;
  }
  public submitpreferences(form: NgForm) {
    var preferences: PreferencesDto = <PreferencesDto>{};
    preferences.maxTemperature = this.highValue;
    preferences.minTemperature = this.value;
    preferences.transportationType = this.selectedTravelType;
    preferences.snowyTrip = this.snow;
    preferences.rainyTrip = this.rain;
    console.log(preferences);
    this.userService.savePreferenecs(preferences);
    this.router.navigateByUrl('mainaccess');
  }
 
}
