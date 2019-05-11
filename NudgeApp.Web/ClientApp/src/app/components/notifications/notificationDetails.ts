import { Component } from '@angular/core';
import { NotificationService, NudgeNotification } from '../../services/NotificationService';
import { ActivatedRoute } from '@angular/router';
import { NotificationDto } from '../../types/NotificationDto';
import { RoadCondition, WindCondition, Probabilities } from '../../types/ForecastDto';
import { NudgeResult } from '../../types/Nudge';

@Component({
  templateUrl: './NotificationDetails.html',
  providers: [NotificationService],
})

export class NotificationDetailsComponent {

  notification: NotificationDto = <NotificationDto>{};
  showData: boolean = false;
  tripData: boolean = false;
  showWeather: boolean = false;
  windCondition : string;
  weather : string;
  roadCondition: string;

  constructor(private route: ActivatedRoute, private notificationService: NotificationService) {
    this.notification.createdOn = new Date(Date.now())
  }

  ngOnInit() {
    var id = this.route.snapshot.paramMap.get("id");
    this.notificationService.NotificationDetails(id).subscribe(result => {

      // @ts-ignore
      result.createdOn = new Date(Date.parse(result.createdOn));
      // @ts-ignore
      result.dateTime = new Date(Date.parse(result.dateTime));
      this.notification = result;
      this.showData = true;
      if (this.notification.duration > 0)
        this.tripData = true;
      this.windCondition = WindCondition[this.notification.windCondition];
      if (this.notification.probability != Probabilities.NotEvaluated) {
        this.weather = Probabilities[this.notification.probability];
        this.showWeather = true;
      }
      this.roadCondition = RoadCondition[this.notification.roadCondition];
    });
  }

  GoodNudge(id: any) {

    this.notification.nudgeResult = NudgeResult.Successful;

    this.notificationService.SetNudge(this.notification.id, NudgeResult.Successful).subscribe();
  }

  BadNudge(id: any) {
    this.notification.nudgeResult = NudgeResult.Failed;

    this.notificationService.SetNudge(this.notification.id, NudgeResult.Failed).subscribe();
  }
}
