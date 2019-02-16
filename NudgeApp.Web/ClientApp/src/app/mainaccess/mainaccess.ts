import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { userservice } from '../signup/userservice';
import { SwPush } from '@angular/service-worker';
import { subscriptionservice } from '../services/SubscriptionService'
import { Subscription } from '../types/Subscription'


@Component({
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [userservice, SwPush, subscriptionservice],
})

export class MainaccessComponent {

  readonly VAPID_PUBLIC_KEY = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";

  lat: number = 51.678418;
  lng: number = 7.809007;
 

  constructor(
    private swPush: SwPush,
    private userService: userservice,
    private subscriptionservice: subscriptionservice) {
    this.subscribeToNotifications();

    navigator.geolocation.getCurrentPosition((position) => {
      console.log(position);
      this.lat = position.coords.latitude;
      this.lng = position.coords.longitude;
    })
  }

  public userLocation(form: NgForm) {


  }
  private subscribeToNotifications() {

    this.swPush.requestSubscription({
      serverPublicKey: this.VAPID_PUBLIC_KEY
    })
      .then(sub => {
        var jsonSub = sub.toJSON();
        var subscription = new Subscription();
        subscription.auth = jsonSub["keys"]["auth"];
        subscription.p256dh = jsonSub["keys"]["p256dh"];
        subscription.endpoint = sub["endpoint"];
        console.log(subscription);
        this.subscriptionservice.addSubscription("lae", subscription);
      })
      .catch(err => console.error("Could not subscribe to notifications", err));
  }

}
