import { Component, OnInit, ViewChild } from '@angular/core';
import { SchedulerEvent } from '@progress/kendo-angular-scheduler';
import { ActivityModel } from '../shared/models/activity.model';
import { BaseComponent } from '../shared/base.component';
import { ActivityModalComponent } from '../activity-modal/activity-modal.component';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { MessageBoxService } from '../services/common/message-box.service';
import { ActivitiesService } from '../services/activities.service';
import { CalendarModel } from '../shared/models/calendar.model';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent extends BaseComponent implements OnInit {

  @ViewChild('activityModal', { static: true }) activityModal: ActivityModalComponent;

  calendar = new CalendarModel();

  // public selectedDate: Date = displayDate;
  public selectedDate: Date = new Date();
  // public events: SchedulerEvent[] = sampleDataWithResources;

  /* public resources: any[] = [
    {
      data: [
        { text: "Meeting Room 101", value: 1, color: "#ffc000" },
        { text: "Meeting Room 201", value: 2, color: "#92d050" },
        { text: "Meeting Room 301", value: 3, color: "#00b0f0" }
      ],
      field: "roomId",
      valueField: "value",
      textField: "text",
      colorField: "color",
    }
  ]; */

  constructor(
    private readonly _messageBox: MessageBoxService,
    private readonly _activitiesService: ActivitiesService
  ) {
    super();
  }

  test(event: any) {
    console.log(event);
  }

  modificaIntervento(event: any) {
    this.activityModal.loadData();
    this._subscriptions.push(
      this._activitiesService.getActivity(event.event.id)
        .pipe(
          switchMap(e => this.activityModal.open(e)),
          tap(x => !x && this._getCalendar()),
          filter(e => e),
          map(() => this.activityModal.options),
          switchMap(e => this._activitiesService.updateActivity(e, e.id)),
          map(() => this.activityModal.options),
          tap(e => this._messageBox.success(`Intervento aggiornato`)),
          tap(() => this._getCalendar())
        )
        .subscribe()
    );
  }

  aggiungiNuovoIntervento() {
    const request = new ActivityModel();
    this.activityModal.loadData();
    this._subscriptions.push(
      this.activityModal.open(request)
        .pipe(
          filter(e => e),
          switchMap(() => this._activitiesService.createActivity(request)),
          tap(e => {
            this._messageBox.success(`Intervento creato`);
          }),
          tap(() => {
            this._getCalendar();
          })
        )
        .subscribe()
    );
  }

  protected _getCalendar() {
    this._subscriptions.push(
      this._activitiesService.getCalendar()
        .pipe(
          tap(e => {
            console.log(e);
            this.calendar = e;
          })
        )
        .subscribe()
    );
  }

  ngOnInit() {
    this._getCalendar();
  }
}


const currentYear = new Date().getFullYear();
const parseAdjust = (eventDate: string): Date => {
  const date = new Date(eventDate);
  date.setFullYear(currentYear);
  return date;
};

const randomInt = (min: any, max: any): number => {
  return Math.floor(Math.random() * (max - min + 1)) + min;
}

export const displayDate = new Date(currentYear, 5, 24);

