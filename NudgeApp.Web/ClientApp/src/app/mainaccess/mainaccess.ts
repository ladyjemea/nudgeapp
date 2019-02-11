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

  constructor(
    private swPush: SwPush,
    private userService: userservice,
    private subscriptionservice: subscriptionservice) {
    this.subscribeToNotifications();
  }

  public userLocation(form: NgForm) {

  }
  private subscribeToNotifications() {

    this.swPush.requestSubscription({
      serverPublicKey: this.VAPID_PUBLIC_KEY
    })
      .then(sub => {
        var subscription = new Subscription();
        var dec = new TextDecoder("utf-8");
        subscription.auth = dec.decode(sub.getKey("auth"));
        subscription.p256dh = dec.decode(sub.getKey("p256dh"));
        subscription.endpoint = sub.endpoint;
        console.log(subscription);
        this.subscriptionservice.addSubscription("lae", subscription);
      })
      .catch(err => console.error("Could not subscribe to notifications", err));
  }

}
