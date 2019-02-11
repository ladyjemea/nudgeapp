import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import 'rxjs';
import { userservice } from '../signup/userservice';
import { SwPush } from '@angular/service-worker';


@Component({
  templateUrl: './mainaccess.html',
  styleUrls: ['./mainaccess.css'],
  providers: [userservice, SwPush],
})

export class MainaccessComponent {

  readonly VAPID_PUBLIC_KEY = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";

  constructor(
    private swPush: SwPush,
    private userService: userservice) {
    this.subscribeToNotifications();
  }

  public userLocation(form: NgForm) {

  }
  private subscribeToNotifications() {

    this.swPush.requestSubscription({
      serverPublicKey: this.VAPID_PUBLIC_KEY
    })
      .then(sub => {
        console.log(sub);
        console.log(sub.getKey("p256dh"));
        console.log(sub.getKey("auth"));
      })
      .catch(err => console.error("Could not subscribe to notifications", err));
  }
 
}
