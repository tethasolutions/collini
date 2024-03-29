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

    readCustomers(state: State, anagraficaType: string) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/${anagraficaType}?${params}`)
            .pipe(
                map(e =>
                    {
                        const customers: Array<CustomerModel> = [];
                        e.data.forEach(item => {
                            const customer: CustomerModel = Object.assign(new CustomerModel(), item);

                            const addresses: Array<AddressModel> = [];
                            customer.addresses.forEach(addressitem => {
                                const address: AddressModel = Object.assign(new AddressModel(), addressitem);
                                addresses.push(address);
                            });
                            customer.addresses = addresses;

                            let mainAddress = addresses.find(x => x.isMainAddress);
                            if (mainAddress == undefined) { mainAddress = new AddressModel(); }
                            customer.mainAddress = mainAddress;
                            customers.push(customer);
                        });
                        return <GridDataResult>{
                            data: hasGroups ? translateDataSourceResultGroups(customers) : customers,
                            total: e.total
                        };
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
                    const customer: CustomerModel = Object.assign(new CustomerModel(), e);

                    const addresses: Array<AddressModel> = [];
                    customer.addresses.forEach(item => {
                        const address: AddressModel = Object.assign(new AddressModel(), item);
                        addresses.push(address);
                    });
                    customer.addresses = addresses;

                    let mainAddress = addresses.find(x => x.isMainAddress);
                    if (mainAddress == undefined) { mainAddress = new AddressModel(); }
                    customer.mainAddress = mainAddress;

                    return customer;
                })
            );
    }
}
