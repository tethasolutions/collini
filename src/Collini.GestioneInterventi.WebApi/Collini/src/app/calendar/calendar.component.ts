import { Component } from '@angular/core';
import { SchedulerEvent } from '@progress/kendo-angular-scheduler';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent {
  
  public selectedDate: Date = displayDate;
  public events: SchedulerEvent[] = sampleDataWithResources;

  public resources: any[] = [
    {
      name: "Rooms",
      data: [
        { text: "Meeting Room 101", value: 1, color: "#ffc000" },
        { text: "Meeting Room 201", value: 2, color: "#92d050" },
        { text: "Meeting Room 301", value: 3, color: "#00b0f0" },
      ],
      field: "roomId",
      valueField: "value",
      textField: "text",
      colorField: "color",
    },
    {
      name: "Attendees",
      data: [
        { text: "Alex", value: 1, color: "#f8a398" },
        { text: "Bob", value: 2, color: "#51a0ed" },
        { text: "Charlie", value: 3, color: "#56ca85" },
      ],
      multiple: true,
      field: "attendees",
      valueField: "value",
      textField: "text",
      colorField: "color",
    },
  ];

  test(event: any) {
    console.log(event);
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
        "Start": "2013-06-19T06:00:00.000Z",
        "End": "2013-06-19T08:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 1,
        "IsAllDay": false,
        "operatore": "Operatore 1",
        "cliente": "Cliente xxx",
        "commessa": "001/2023"
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
        "commessa": "003/2023"
    },
    {
        "TaskID": 123,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-20T06:00:00.000Z",
        "End": "2013-06-20T08:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 2,
        "IsAllDay": false,
        "operatore": "Operatore 2",
        "cliente": "Cliente xxx",
        "commessa": "002/2023"
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
        "commessa": "004/2023"
    },
    {
        "TaskID": 125,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-21T06:30:00.000Z",
        "End": "2013-06-21T08:30:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 1,
        "IsAllDay": false,
        "operatore": "Oper. 1",
        "cliente": "Cliente xxx",
        "commessa": "005/2023"
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
        "commessa": "006/2023"
    },
    {
        "TaskID": 127,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-23T06:00:00.000Z",
        "End": "2013-06-23T11:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 3,
        "IsAllDay": false,
        "operatore": "Oper. 3",
        "cliente": "Cliente xxx",
        "commessa": "007/2023"
    },
    {
        "TaskID": 127,
        "OwnerID": 2,
        "Title": "Bowling tournament",
        "Description": "",
        "StartTimezone": null,
        "Start": "2013-06-23T06:00:00.000Z",
        "End": "2013-06-23T11:00:00.000Z",
        "EndTimezone": null,
        "RecurrenceRule": null,
        "RecurrenceID": null,
        "RecurrenceException": null,
        "RoomID": 2,
        "IsAllDay": false,
        "operatore": "Oper. 2",
        "cliente": "Cliente xxx",
        "commessa": "008/2023"
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

export const sampleData = baseData.map(dataItem => (
    <SchedulerEvent> {
        id: dataItem.TaskID,
        start: parseAdjust(dataItem.Start),
        startTimezone: dataItem.startTimezone,
        end: parseAdjust(dataItem.End),
        endTimezone: dataItem.endTimezone,
        isAllDay: dataItem.IsAllDay,
        title: dataItem.Title,
        description: dataItem.Description,
        recurrenceRule: dataItem.RecurrenceRule,
        recurrenceId: dataItem.RecurrenceID,
        recurrenceException: dataItem.RecurrenceException,

        roomId: dataItem.RoomID,
        ownerID: dataItem.OwnerID,
        
        operatore: dataItem.operatore,
        cliente: dataItem.cliente,
        commessa: dataItem.commessa
    }
));

export const sampleDataWithResources = baseData.map(dataItem => (
    <SchedulerEvent> {
        id: dataItem.TaskID,
        start: parseAdjust(dataItem.Start),
        startTimezone: dataItem.startTimezone,
        end: parseAdjust(dataItem.End),
        endTimezone: dataItem.endTimezone,
        isAllDay: dataItem.IsAllDay,
        title: dataItem.Title,
        description: dataItem.Description,
        recurrenceRule: dataItem.RecurrenceRule,
        recurrenceId: dataItem.RecurrenceID,
        recurrenceException: dataItem.RecurrenceException,
        operatore: dataItem.operatore,
        cliente: dataItem.cliente,
        commessa: dataItem.commessa,
        // roomId: randomInt(1, 2),
        roomId: dataItem.RoomID,
        attendees: [randomInt(1, 3)]
    }
));

export const sampleDataWithCustomSchema = baseData.map(dataItem => (
    {
        ...dataItem,
        Start: parseAdjust(dataItem.Start),
        End: parseAdjust(dataItem.End)
    }
));