import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
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
import { JobBusService } from './job-bus.service';
import { CopyActivityModel } from '../shared/models/copy-activity.model';

@Injectable()
export class ActivitiesService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/activities`;

    constructor(
        private readonly _http: HttpClient,
        private readonly _bus: JobBusService
    ) {}

    readActivities(state: State) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/activities?${params}`)
            .pipe(
                map(e =>
                    {
                        const activities: Array<ActivityModel> = [];
                        e.data.forEach(item => {
                            const activity: ActivityModel = Object.assign(new ActivityModel(), item);

                            activities.push(activity);
                        });
                        return <GridDataResult>{
                            data: hasGroups ? translateDataSourceResultGroups(activities) : activities,
                            total: e.total
                        };
                    }
                )
            );
    }

    getActivity(id: number) {
        return this._http.get<ActivityModel>(`${this._baseUrl}/activity/${id}`)
            .pipe(
                map(e => {
                    const activity = Object.assign(new ActivityModel(), e);
                    activity.start = new Date(activity.start);
                    activity.end = new Date(activity.end);
                    return activity;
                })
            );
    }

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
                            id: dataItem.id,
                            start: new Date(dataItem.start),
                            end: new Date(dataItem.end),
                            isAllDay: false,
                            title: dataItem.description,
                            description: dataItem.description,
                            operatorId: dataItem.operatorId,
                            cliente: dataItem.customerName,
                            commessa: dataItem.jobCode,
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
                tap(() => this._bus.jobUpdated())
            );
    }

    updateActivity(request: ActivityModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/activity/${id}`, request)
            .pipe(
                map(() => { }),
                tap(() => this._bus.jobUpdated())
            );
    }
    
    copyActivity(request: CopyActivityModel) {
        return this._http.put<void>(`${this._baseUrl}/copyactivity/`, request)
            .pipe(
                tap(() => this._bus.jobUpdated())
            );
    }
    
    payJob(activityId: number) {
        return this._http.get<void>(`${this._baseUrl}/payjob/${activityId}`)
            .pipe(
                map(() => { })
            );
    }

    saveAndQuotation(request: ActivityModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/saveandquotation/${id}`, request)
            .pipe(
                map(() => { }),
                tap(() => this._bus.jobUpdated())
            );
    }

    deleteActivity(id: number) {
        return this._http.delete<void>(`${this._baseUrl}/activity/${id}`)
            .pipe(
                map(() => { })
            );
    }
}
