import {Component, ChangeDetectionStrategy, ViewChild, TemplateRef} from '@angular/core';
import {startOfDay, endOfDay, subDays, addDays, endOfMonth, isSameDay, isSameMonth, addHours} from 'date-fns';
import { Subject } from 'rxjs';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import {CalendarEvent, CalendarEventAction, CalendarEventTimesChangedEvent, CalendarView} from 'angular-calendar';


const colors: any = {
  blue: {
    primary: '#1e90ff',
    secondary: '#D1E8FF'
  }
};

@Component({
  selector: 'mwl-demo-component',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './calendar.html'
})
export class CalendarComponent {
  @ViewChild('modalContent') modalContent: TemplateRef<any>;

  view: CalendarView = CalendarView.Month;

  CalendarView = CalendarView;

  viewDate: Date = new Date();

  modalData: {
    action: string;
    event: CalendarEvent;
  };

  actions: CalendarEventAction[] = [
    //{
    //  label: '<i class="fa fa-fw fa-pencil"></i>',
    //  onClick: ({ event }: { event: CalendarEvent }): void => {
    //    this.handleEvent('Edited', event);
    //  }
    //},
    //{
    //  label: '<i class="fa fa-fw fa-times"></i>',
    //  onClick: ({ event }: { event: CalendarEvent }): void => {
    //    this.events = this.events.filter(iEvent => iEvent !== event);
    //    this.handleEvent('Deleted', event);
    //  }
    //}
  ];

  refresh: Subject<any> = new Subject();

  events: CalendarEvent[] = [
    {
      start: subDays(startOfDay(new Date()), 1),
      end: addDays(new Date(), 1),
      title: 'A 2 hour event',
      color: colors.red,
      actions: this.actions,
      allDay: false,
    },
    {
      start: startOfDay(new Date()),
      title: 'An 3 day event',
      color: colors.yellow,
      actions: this.actions
    },
    
  ];

}
