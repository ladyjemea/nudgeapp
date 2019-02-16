import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

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
import { ServiceWorkerModule } from '@angular/service-worker';

import { environment } from 'src/environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LoginComponent,
    SignupComponent,
    TravelComponent,
    MainaccessComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ServiceWorkerModule.register('/ngsw-worker.js', { enabled: true }),
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'login', component: LoginComponent },
      { path: 'signup', component: SignupComponent },
      { path: 'travelnow', component: TravelComponent },
      { path: 'mainaccess', component: MainaccessComponent}
     
    ]),
    AgmCoreModule.forRoot({
      apiKey: 'AIzaSyCCbgVPkBgwul0cofmo-VSMOefNSzrAOEo',
      libraries: ['geometry']
    })
  ],

  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
