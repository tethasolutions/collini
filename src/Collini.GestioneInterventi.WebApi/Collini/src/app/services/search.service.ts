import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { ContactAddressModel } from '../shared/models/contact-address.model';
import { JobModel } from '../shared/models/job.model';
import { ContactModel } from '../shared/models/contact.model';
import { JobCountersModel } from "../shared/models/job-counters.model";
import { JobOperatorModel } from '../shared/models/job-operator.model';
import { AddressModel } from '../shared/models/address.model';
import { CustomerModel } from '../shared/models/customer.model';
import { JobSourceModel } from '../shared/models/job-source.model';
import { ProductTypeModel } from '../shared/models/product-type.model';
import { JobDetailModel } from '../shared/models/job-detail.model';
import { JobBusService } from './job-bus.service';

@Injectable()
export class SearchService {

    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/jobs`;

    constructor(
        private readonly _http: HttpClient,
        private readonly _bus: JobBusService
        
    ) {}

    readJobs(state: State) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/jobs-search?${params}`)
            .pipe(
                map(e =>
                    {
                        const jobs: Array<JobDetailModel> = [];
                        e.data.forEach(item => {
                            const job: JobDetailModel = Object.assign(new JobDetailModel(), item);
                            job.expirationDate = new Date(job.expirationDate);
                            job.jobDate = new Date(job.jobDate);
                            job.customer = Object.assign(new CustomerModel(), job.customer);

                            const addresses: Array<AddressModel> = [];
                            job.customer.addresses.forEach(addressItem => {
                                const address: AddressModel = Object.assign(new AddressModel(), addressItem);
                                addresses.push(address);
                            });
                            job.customer.addresses = addresses;

                            const mainAddress = addresses.find(x => x.isMainAddress == true);
                            if (mainAddress != undefined) {
                                job.customerAddress = mainAddress;
                            }

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

}
