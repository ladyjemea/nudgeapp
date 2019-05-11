import { Component } from '@angular/core';
import { NotificationService, NudgeNotification } from '../../services/NotificationService';
import { NudgeResult } from '../../types/Nudge';
import { Router } from '@angular/router';

@Component({
  templateUrl: './notificationHistory.html',
  providers: [NotificationService],
})

export class NotificationHistoryComponent {

  notifications: Array<NudgeNotification> = [];

  constructor(private notificationService: NotificationService, private router: Router) {
    this.GetNotifications();
  }

  GoodNudge(event, id: any) {
    event.stopPropagation();

    this.notifications.forEach(not => {
      if (not.id == id) {
        not.nudgeResut = NudgeResult.Successful;
      }
    });

    this.notificationService.SetNudge(id, NudgeResult.Successful).subscribe(result => this.GetNotifications());
  }

  BadNudge(event, id: any) {
    event.stopPropagation();

    this.notifications.forEach(not => {
      if (not.id == id) {
        not.nudgeResut = NudgeResult.Failed;
      }
    });

    this.notificationService.SetNudge(id, NudgeResult.Failed).subscribe(result => this.GetNotifications());
  }

  Details(id: any) {
    this.router.navigateByUrl('/notification/' + id);
  }

  private GetNotifications() {
    this.notificationService.GetAllNotifications().subscribe((result: Array<NudgeNotification>) => {
      result.forEach(not => {
        // @ts-ignore
        not.createdOn = new Date(Date.parse(not.createdOn));
      });

      this.notifications = result;
    });
  }
}
