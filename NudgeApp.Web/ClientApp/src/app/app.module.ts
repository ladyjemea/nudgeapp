import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule, Routes, Route } from '@angular/router';
import { CommonModule } from '@angular/common';

import { SocialLoginModule, AuthServiceConfig, GoogleLoginProvider } from "angular-6-social-login";

import { AgmCoreModule } from '@agm/core';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login';
import { SignupComponent } from './components/signup/signup';
import { TravelComponent } from './components/travelnow/travelnow';
import { MainaccessComponent } from './components/mainaccess/mainaccess';
//import { CalendarComponent } from './components/calendar/calendar';
import { MainDisplayComponent } from './components/maindisplay/maindisplay';
import { ServiceWorkerModule } from '@angular/service-worker';

import { JwtInterceptor } from './services/JwtInterceptor';

export function getAuthServiceConfigs() {
  let config = new AuthServiceConfig(
    [
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider("600101543512-tnhs33cfbs09rqd6no8tajg5ooccoa0q.apps.googleusercontent.com")
      }]);
  return config;
}

//const appRoutes: Routes = [
//  {
//    path: "search",
//    component: MainDisplayComponent,
//  }]

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    TravelComponent,
    MainaccessComponent,
    //CalendarComponent,
    MainDisplayComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    CommonModule,
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: true }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: SignupComponent },
      { path: 'travelnow', component: TravelComponent },
      { path: 'mainaccess', component: MainaccessComponent },
      //{ path: 'calendar', component: CalendarComponent },
      { path: 'search', component: MainDisplayComponent },
      { path: 'maindisplay', component: MainDisplayComponent }

    ]),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCCbgVPkBgwul0cofmo-VSMOefNSzrAOEo',
      libraries: ['geometry']
    })
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: AuthServiceConfig, useFactory: getAuthServiceConfigs }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
