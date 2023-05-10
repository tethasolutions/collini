import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { AddressModel } from '../shared/models/address.model';
import { CustomerModel } from '../shared/models/customer.model';

@Injectable()
export class CustomerService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/customers`;

    constructor(
        private readonly _http: HttpClient
    ) {}

    readCustomers(state: State) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/customers?${params}`)
            .pipe(
                map(e =>
                    <GridDataResult>{
                        data: hasGroups ? translateDataSourceResultGroups(e.data) : e.data,
                        total: e.total
                    }
                )
            );
    }

    createCustomer(request: CustomerModel) {
        return this._http.post<CustomerModel>(`${this._baseUrl}/customer`, request)
            .pipe(
                map(e => {
                    return e;
                })
            );
    }

    updateCustomer(request: CustomerModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/customer/${id}`, request)
            .pipe(
                map(() => { })
            );
    }

    deleteCustomer(id: number) {
        return this._http.delete<void>(`${this._baseUrl}/customer/${id}`)
            .pipe(
                map(() => { })
            );
    }

    getCustomer(id: number) {
        return this._http.get<CustomerModel>(`${this._baseUrl}/customer/${id}`)
            .pipe(
                map(e => {
                    const customer = Object.assign(new CustomerModel(), e);
                    return customer;
                })
            );
    }
}
