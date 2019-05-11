import { Component } from '@angular/core';
import { Options } from 'ng5-slider';

@Component({
  selector: 'app-range-slider',
  templateUrl: './preferences.html',
})

export class PreferencesComponent {
  value: number = -50;
  highValue: number = 50;
  options: Options = {
    floor: -50,
    ceil: 50
  };
}
