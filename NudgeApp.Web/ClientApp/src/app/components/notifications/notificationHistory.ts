import { Component } from '@angular/core';
import { NotificationService, NudgeNotification } from '../../services/NotificationService';
import { NudgeResult } from '../../types/Nudge';

@Component({
  selector: 'app-home',
  templateUrl: './notificationHistory.html',
  providers: [NotificationService],
})

export class NotificationHistoryComponent {

  notifications: Array<NudgeNotification> = [];

  constructor(private notificationService: NotificationService) {
    this.GetNotifications();
  }

  GoodNudge(id: any) {
    this.notifications.forEach(not => {
      if (not.id == id) {
        not.nudgeResut = NudgeResult.Successful;
      }
    });

    this.notificationService.SetNudge(id, NudgeResult.Successful).subscribe(result => this.GetNotifications());
  }

  BadNudge(id: any) {
    this.notifications.forEach(not => {
      if (not.id == id) {
        not.nudgeResut = NudgeResult.Failed;
      }
    });

    this.notificationService.SetNudge(id, NudgeResult.Failed).subscribe(result => this.GetNotifications());
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
