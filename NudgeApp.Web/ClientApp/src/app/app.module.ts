import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes, Route } from '@angular/router';
import { CommonModule } from '@angular/common';

import { AgmCoreModule } from '@agm/core';

import {
  GoogleApiModule,
  GoogleApiService,
  GoogleAuthService,
  NgGapiClientConfig,
  NG_GAPI_CONFIG,
  GoogleApiConfig
} from "ng-gapi";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login';
import { SignupComponent } from './components/signup/signup';
import { MainaccessComponent } from './components/mainaccess/mainaccess';
import { FeedbackComponent } from './components/feedback/feedback';
import { TravelNowComponent } from './components/travelnow/travelnow';
import { ServiceWorkerModule } from '@angular/service-worker';
import { JwtInterceptor } from './services/JwtInterceptor';
import { NotificationHistoryComponent } from './components/notifications/notificationHistory';

let gapiClientConfig: NgGapiClientConfig = {
  client_id: "600101543512-tnhs33cfbs09rqd6no8tajg5ooccoa0q.apps.googleusercontent.com",
  discoveryDocs: ["https://analyticsreporting.googleapis.com/$discovery/rest?version=v4"],
  scope: [
    "https://www.googleapis.com/auth/calendar.readonly"
  ].join(" ")
};

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    MainaccessComponent,
    FeedbackComponent,
    TravelNowComponent,
    NotificationHistoryComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    GoogleApiModule.forRoot({
      provide: NG_GAPI_CONFIG,
      useValue: gapiClientConfig
    }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: true }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: SignupComponent },
      { path: 'mainaccess', component: MainaccessComponent },
      { path: 'feedback', component: FeedbackComponent },
      { path: 'search', component: TravelNowComponent },
      { path: 'notificationHistory', component: NotificationHistoryComponent }

    ]),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCCbgVPkBgwul0cofmo-VSMOefNSzrAOEo',
      libraries: ['geometry']
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
