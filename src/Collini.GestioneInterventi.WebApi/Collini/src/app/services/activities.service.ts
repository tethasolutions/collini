import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { AddressModel } from '../shared/models/address.model';
import { CustomerModel } from '../shared/models/customer.model';
import { ActivityModel } from '../shared/models/activity.model';

@Injectable()
export class ActivitiesService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/activities`;

    constructor(
        private readonly _http: HttpClient
    ) {}

    createActivity(request: ActivityModel) {
        return this._http.post<number>(`${this._baseUrl}/activity`, request)
            .pipe(
                map(e => {
                    return e;
                })
            );
    }
}
