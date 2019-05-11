import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import 'rxjs';
import { Observable } from 'rxjs';
import { NudgeResult } from '../types/Nudge';
import { NotificationDto } from '../types/NotificationDto';

@Injectable()
export class NotificationService {

  constructor(private http: HttpClient) { }

  public GetAllNotifications() {
    return this.http.get('Notification/GetAll', { responseType: "json" });
  }

  public SetNudge(id: any, nudgeResult: NudgeResult): Observable<any> {
    var params = new HttpParams();
    params = params.append('notificationId', id);
    params = params.append('nudgeResult', nudgeResult.toString());

    return this.http.get('Notification/Set', { params: params });
  }

  public NotificationDetails(id: any): Observable<NotificationDto> {
    var params = new HttpParams();
    params = params.append('notificationId', id);

    return <Observable<NotificationDto>>this.http.get('Notification/Details', { params: params });
  }
}

export interface NudgeNotification {
  id: any,
  status: NotificationStatus,
  createdOn: Date,
  text: string,
  nudgeResult: NudgeResult
}

export enum NotificationStatus {
  Waiting,
  Set
}
