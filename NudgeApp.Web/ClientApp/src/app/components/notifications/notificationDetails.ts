import { Component } from '@angular/core';
import { NotificationService, NudgeNotification } from '../../services/NotificationService';
import { NudgeResult } from '../../types/Nudge';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './NotificationDetails.html',
  providers: [NotificationService],
})

export class NotificationDetailsComponent {

  notification: NudgeNotification = <NudgeNotification>{};

  constructor(private route: ActivatedRoute, private notificationService: NotificationService) {
    this.notification.createdOn = new Date(Date.now())
  }

  ngOnInit() {
    var id = this.route.snapshot.paramMap.get("id");
    this.notificationService.NotificationDetails(id).subscribe(result => {

      // @ts-ignore
      result.createdOn = new Date(Date.parse(result.createdOn));
      result.dateTime = new Date(Date.parse(result.dateTime));
      this.notification = result;
    });
  }
}
