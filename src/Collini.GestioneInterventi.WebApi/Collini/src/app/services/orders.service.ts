import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap } from 'rxjs/operators';
import { ApiUrls } from './common/api-urls';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State, toDataSourceRequestString, translateDataSourceResultGroups } from '@progress/kendo-data-query';
import { ContactAddressModel } from '../shared/models/contact-address.model';
import { JobModel } from '../shared/models/job.model';
import { ContactModel } from '../shared/models/contact.model';
import { OrderModel } from '../shared/models/order.model';
import { AddressModel } from '../shared/models/address.model';
import { CustomerModel } from '../shared/models/customer.model';
import { OrderDetailModel } from '../shared/models/order-detail.model';
import { JobBusService } from './job-bus.service';

@Injectable()
export class OrdersService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/orders`;

    constructor(
        private readonly _http: HttpClient,
        private readonly _bus: JobBusService
    ) {}

    readOrders(state: State) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/orders?${params}`)
            .pipe(
                map(e =>
                    {
                        const orders: Array<OrderModel> = [];
                        e.data.forEach(item => {
                            const order: OrderModel = Object.assign(new OrderModel(), item);
                            order.createdOn = new Date(order.createdOn);
                            order.expirationDate = new Date(order.expirationDate);

                            orders.push(order);
                        });
                        return <GridDataResult>{
                            data: hasGroups ? translateDataSourceResultGroups(orders) : orders,
                            total: e.total
                        };
                    }
                )
            );
    }

    getOrderDetail(id: number) {
        return this._http.get<OrderDetailModel>(`${this._baseUrl}/order-detail/${id}`)
            .pipe(
                map(response => {
                    const order: OrderDetailModel = Object.assign(new OrderDetailModel(), response);

                    order.expirationDate = new Date(order.expirationDate);

                    return order;
                }) 
            );
    }

    createOrder(request: OrderDetailModel) {
        return this._http.post<OrderDetailModel>(`${this._baseUrl}/create-order`, request)
            .pipe(
                tap(() => this._bus.jobUpdated())
            );
    }

    updateOrder(request: OrderDetailModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/update-order/${id}`, request)
            .pipe(
                map(() => { }),
                tap(() => this._bus.jobUpdated())
            );
    }

    getAllOrders() {
        return this._http.get<Array<OrderModel>>(`${this._baseUrl}/all-orders`)
            .pipe(
                map(result =>
                    {
                        const orders: Array<OrderModel> = [];
                        result.forEach(item => {
                            const order: OrderModel = Object.assign(new OrderModel(), item);
                            order.expirationDate = new Date(order.expirationDate);

                            orders.push(order);
                        });
                        return orders;
                    }
                )
            );
    }
}
