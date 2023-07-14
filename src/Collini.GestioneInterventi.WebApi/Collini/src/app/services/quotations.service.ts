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
import { QuotationModel } from '../shared/models/quotation.model';
import { AddressModel } from '../shared/models/address.model';
import { CustomerModel } from '../shared/models/customer.model';
import { JobSourceModel } from '../shared/models/job-source.model';
import { ProductTypeModel } from '../shared/models/product-type.model';
import { JobDetailModel } from '../shared/models/job-detail.model';
import { QuotationDetailModel } from '../shared/models/quotation-detail.model';
import { JobBusService } from './job-bus.service';

@Injectable()
export class QuotationsService {
    
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/quotations`;

    constructor(
        private readonly _http: HttpClient,
        private readonly _bus: JobBusService
    ) {}

    readQuotations(state: State) {
        const params = toDataSourceRequestString(state);
        const hasGroups = state.group && state.group.length;

        return this._http.get<GridDataResult>(`${this._baseUrl}/quotations?${params}`)
            .pipe(
                map(e =>
                    {
                        const quotations: Array<QuotationModel> = [];
                        e.data.forEach(item => {
                            const quotation: QuotationModel = Object.assign(new QuotationModel(), item);
                            quotation.expirationDate = new Date(quotation.expirationDate);

                            quotations.push(quotation);
                        });
                        return <GridDataResult>{
                            data: hasGroups ? translateDataSourceResultGroups(quotations) : quotations,
                            total: e.total
                        };
                    }
                )
            );
    }

    getQuotationDetail(id: number) {
        return this._http.get<QuotationDetailModel>(`${this._baseUrl}/quotation-detail/${id}`)
            .pipe(
                map(response => {
                    const quotation: QuotationDetailModel = Object.assign(new QuotationDetailModel(), response);

                    quotation.expirationDate = new Date(quotation.expirationDate);

                    return quotation;
                })
            );
    }

    createQuotation(request: QuotationDetailModel) {
        return this._http.post<QuotationDetailModel>(`${this._baseUrl}/create-quotation`, request)
            .pipe(
                tap(() => this._bus.jobUpdated())
            );
    }

    updateQuotation(request: QuotationDetailModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/update-quotation/${id}`, request)
            .pipe(
                map(() => { }),
                tap(() => this._bus.jobUpdated())
            );
    }

    getAllQuotations() {
        return this._http.get<Array<QuotationModel>>(`${this._baseUrl}/all-quotations`)
            .pipe(
                map(result =>
                    {
                        const quotations: Array<QuotationModel> = [];
                        result.forEach(item => {
                            const quotation: QuotationModel = Object.assign(new QuotationModel(), item);
                            quotation.expirationDate = new Date(quotation.expirationDate);

                            quotations.push(quotation);
                        });
                        return quotations;
                    }
                )
            );
    }
}
