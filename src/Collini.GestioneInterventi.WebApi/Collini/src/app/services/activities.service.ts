import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { AddressModel } from '../shared/models/address.model';
import { ActivityCalendarModel } from '../shared/models/activity-calendar.model';
import { ActivityModel } from '../shared/models/activity.model';
import { CalendarModel } from '../shared/models/calendar.model';
import { CalendarResourceModel } from '../shared/models/calendar-resource.model';
import { CalendarResourcesSettingsModel } from '../shared/models/calendar-resources-settings.model';
import { SchedulerEvent } from '@progress/kendo-angular-scheduler';
import { JobModel } from '../shared/models/job.model';

@Injectable()
export class ActivitiesService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/activities`;

    constructor(
        private readonly _http: HttpClient
    ) {}

    getCalendar() {
        return this._http.get<CalendarModel>(`${this._baseUrl}/calendar`)
            .pipe(
                map((e: any) => {
                    const calendar = new CalendarModel();

                    const activities: Array<ActivityCalendarModel> = [];
                    e.activities.forEach((item: ActivityCalendarModel) => {
                        const activity: ActivityCalendarModel = Object.assign(new ActivityCalendarModel(), item);

                        activities.push(activity);
                    });

                    const events = activities.map(dataItem => (
                        <SchedulerEvent> {
                            start: new Date(dataItem.start),
                            end: new Date(dataItem.end),
                            isAllDay: false,
                            title: dataItem.description,
                            description: dataItem.description,
                            operatorId: dataItem.operatorId,
                            cliente: dataItem.customer,
                            commessa: dataItem.job,
                            status: dataItem.status
                        }
                    ));

                    calendar.activities = events;

                    const calendarResources: Array<any> = [];
                    e.resources.forEach((item: any) => {
                        // const resource: any = Object.assign(new any(), item);
                        calendarResources.push(item);
                    });

                    const calendarResourcesSettings = new CalendarResourcesSettingsModel();
                    calendarResourcesSettings.data = calendarResources;
                    calendarResourcesSettings.field = 'operatorId';
                    calendarResourcesSettings.valueField = 'id';
                    calendarResourcesSettings.textField = 'description';
                    calendarResourcesSettings.colorField = 'color';

                    calendar.resourcesSettings = [calendarResourcesSettings];
                    
                    return calendar;
                })
            );
    }

    createActivity(request: ActivityModel) {
        return this._http.post<number>(`${this._baseUrl}/activity`, request)
            .pipe(
                map(e => {
                    return e;
                })
            );
    }
}
