import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FlatpickrModule } from 'angularx-flatpickr';
import { CalendarModule, DateAdapter } from 'angular-calendar';
import { adapterFactory } from 'angular-calendar/date-adapters/date-fns';
import { NgbModalModule } from '@ng-bootstrap/ng-bootstrap';
//import { DemoComponent } from './component';

import { AgmCoreModule } from '@agm/core';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LoginComponent } from './login/login';
import { SignupComponent } from './signup/signup';
import { TravelComponent } from './travelnow/travelnow';
import { MainaccessComponent } from './mainaccess/mainaccess';
import { CalendarComponent } from './calendar/calendar';
import { ServiceWorkerModule } from '@angular/service-worker';

import { JwtInterceptor } from './services/JwtInterceptor';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    TravelComponent,
    MainaccessComponent,
    CalendarComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    NgbModalModule,
    FlatpickrModule.forRoot(),
    CalendarModule.forRoot({provide: DateAdapter, useFactory: adapterFactory}),
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: true }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: SignupComponent },
      { path: 'travelnow', component: TravelComponent },
      { path: 'mainaccess', component: MainaccessComponent },
      { path: 'calendar', component: CalendarComponent }
     
    ]),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCCbgVPkBgwul0cofmo-VSMOefNSzrAOEo',
      libraries: ['geometry']
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
