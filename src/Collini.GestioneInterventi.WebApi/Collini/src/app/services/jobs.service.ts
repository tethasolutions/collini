import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { ContactAddressModel } from '../shared/models/contact-address.model';
import { JobModel } from '../shared/models/job.model';
import { ContactModel } from '../shared/models/contact.model';
import { JobCountersModel } from "../shared/models/job-counters.model";

@Injectable()
export class JobsService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/jobs`;

    constructor(
        private readonly _http: HttpClient
    ) {}

    readJobs(state: State, jobType: string) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/jobs-${jobType}?${params}`)
            .pipe(
                map(e =>
                    {
                        const jobs: Array<JobModel> = [];
                        e.data.forEach(item => {
                            const job: JobModel = Object.assign(new JobModel(), item);
                            job.expirationDate = new Date(job.expirationDate);
                            job.createdOn = new Date(job.createdOn);
                            job.customer = Object.assign(new ContactModel(), job.customer);

                            const addresses: Array<ContactAddressModel> = [];
                            job.customer.addresses.forEach(addressItem => {
                                const address: ContactAddressModel = Object.assign(new ContactAddressModel(), addressItem);
                                addresses.push(address);
                            });
                            job.customer.addresses = addresses;
                            jobs.push(job);
                        });
                        return <GridDataResult>{
                            data: hasGroups ? translateDataSourceResultGroups(jobs) : jobs,
                            total: e.total
                        };
                    }
                )
            );
    }

    getJobCounters() {
        return this._http.get<JobCountersModel>(`${this._baseUrl}/job-counters`)
            .pipe(
                map(e => {
                    const jobCounters: JobCountersModel = Object.assign(new JobCountersModel(), e);
                    return jobCounters;
                })
            );
    }
}
