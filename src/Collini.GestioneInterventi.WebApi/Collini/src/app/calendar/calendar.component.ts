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
            map(e => {
              return e;
            }),
            switchMap(e => this.activityModal.open(e)),
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

/* eslint-disable */

const baseData: any[] = [
    {
        "TaskID": 121,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-19T07:00:00.000Z",
        "End": "2013-06-19T08:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 1,
        "IsAllDay": false,
        "operatore": "Operatore 1",
        "cliente": "Cliente xxx",
        "commessa": "001/2023",
        "color": "red"
    },
    {
        "TaskID": 122,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-19T09:00:00.000Z",
        "End": "2013-06-19T11:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 3,
        "IsAllDay": false,
        "operatore": "Operatore 3",
        "cliente": "Cliente xxx",
        "commessa": "003/2023",
        "color": "blue"
    },
    {
        "TaskID": 123,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-20T07:00:00.000Z",
        "End": "2013-06-20T08:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 2,
        "IsAllDay": false,
        "operatore": "Operatore 2",
        "cliente": "Cliente xxx",
        "commessa": "002/2023",
        "color": "green"
    },
    {
        "TaskID": 124,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-20T08:30:00.000Z",
        "End": "2013-06-20T10:30:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 1,
        "IsAllDay": false,
        "operatore": "Operatore 1",
        "cliente": "Cliente xxx",
        "commessa": "004/2023",
        "color": "red"
    },
    {
        "TaskID": 125,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-21T07:30:00.000Z",
        "End": "2013-06-21T08:30:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 1,
        "IsAllDay": false,
        "operatore": "Oper. 1",
        "cliente": "Cliente xxx",
        "commessa": "005/2023",
        "color": "green"
    },
    {
        "TaskID": 126,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-21T07:00:00.000Z",
        "End": "2013-06-21T09:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 3,
        "IsAllDay": false,
        "operatore": "Oper. 3",
        "cliente": "Cliente xxx",
        "commessa": "006/2023",
        "color": "red"
    },
    {
        "TaskID": 127,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-23T07:00:00.000Z",
        "End": "2013-06-23T12:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 3,
        "IsAllDay": false,
        "operatore": "Oper. 3",
        "cliente": "Cliente xxx",
        "commessa": "007/2023",
        "color": "blue"
    },
    {
        "TaskID": 127,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-23T07:00:00.000Z",
        "End": "2013-06-23T12:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 2,
        "IsAllDay": false,
        "operatore": "Oper. 2",
        "cliente": "Cliente xxx",
        "commessa": "008/2023",
        "color": "red"
    }
];

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

// export const sampleData = baseData.map(dataItem => (
//     <SchedulerEvent> {
//         id: dataItem.TaskID,
//         start: parseAdjust(dataItem.Start),
//         startTimezone: dataItem.startTimezone,
//         end: parseAdjust(dataItem.End),
//         endTimezone: dataItem.endTimezone,
//         isAllDay: dataItem.IsAllDay,
//         title: dataItem.Title,
//         description: dataItem.Description,
//         recurrenceRule: dataItem.RecurrenceRule,
//         recurrenceId: dataItem.RecurrenceID,
//         recurrenceException: dataItem.RecurrenceException,

//         roomId: dataItem.RoomID,
//         ownerID: dataItem.OwnerID,
        
//         operatore: dataItem.operatore,
//         cliente: dataItem.cliente,
//         commessa: dataItem.commessa,
//         color: dataItem.color
//     }
// ));

// export const sampleDataWithResources = baseData.map(dataItem => (
//     <SchedulerEvent> {
//         id: dataItem.TaskID,
//         start: parseAdjust(dataItem.Start),
//         startTimezone: dataItem.startTimezone,
//         end: parseAdjust(dataItem.End),
//         endTimezone: dataItem.endTimezone,
//         isAllDay: dataItem.IsAllDay,
//         title: dataItem.Title,
//         description: dataItem.Description,
//         recurrenceRule: dataItem.RecurrenceRule,
//         recurrenceId: dataItem.RecurrenceID,
//         recurrenceException: dataItem.RecurrenceException,
//         operatore: dataItem.operatore,
//         cliente: dataItem.cliente,
//         commessa: dataItem.commessa,
//         // roomId: randomInt(1, 2),
//         roomId: dataItem.RoomID,
//         attendees: [randomInt(1, 3)],
//         color: dataItem.color
//     }
// ));

// export const sampleDataWithCustomSchema = baseData.map(dataItem => (
//     {
//         ...dataItem,
//         Start: parseAdjust(dataItem.Start),
//         End: parseAdjust(dataItem.End)
//     }
// ));