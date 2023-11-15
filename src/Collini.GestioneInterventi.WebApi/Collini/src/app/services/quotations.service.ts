import { Injectable } from '@angular/core';
import { HttpClient, HttpEventType, HttpRequest, HttpResponse } from '@angular/common/http';
import { filter, map, tap } from 'rxjs/operators';
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
import { QuotationAttachmentModel } from '../shared/models/quotation-attachment.model';
import { QuotationAttachmentUploadFileModel } from '../shared/models/quotation-attachment-upload-file.model';

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
                            quotation.createdOn = new Date(quotation.createdOn);
                            quotation.jobDate = new Date(quotation.jobDate);
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

    createQuotationAttachment(request: QuotationAttachmentModel) {
        return this._http.post<QuotationAttachmentModel>(`${this._baseUrl}/create-attachment`, request)
            .pipe(               
            );
    }

    updateQuotationAttachment(request: QuotationAttachmentModel, id: number) {
        return this._http.put<void>(`${this._baseUrl}/update-attachment/${id}`, request)
            .pipe(              
            );
            }

    getQuotationAttachments( id: number) {
        return this._http.get<Array<QuotationAttachmentModel>>(`${this._baseUrl}/${id}/all-attachments`)
            .pipe(
                map(result =>
                    {
                        const quotationAttachments: Array<QuotationAttachmentModel> = [];
                        result.forEach(item => {
                            const quotation: QuotationAttachmentModel = Object.assign(new QuotationAttachmentModel(), item);
                            quotationAttachments.push(quotation);
                        });
                        return quotationAttachments;
                    }
                )
            );
    }

    getQuotationAttachmentDetail(id: number) {
        return this._http.get<QuotationDetailModel>(`${this._baseUrl}/attachment-detail/${id}`)
            .pipe(
                map(response => {
                   const quotationAttachment: QuotationDetailModel = Object.assign(new QuotationDetailModel(), response);
                    return quotationAttachment;
                })
            );
    }

    uploadQuotationAttachmentFile(file: File) {
        const formData = new FormData();

        formData.append(file.name, file);

        const uploadReq = new HttpRequest("POST",
            `${this._baseUrl}/quotation-attachment/upload-file`,
            formData,
            {
                reportProgress: false
            });

        return this._http.request(uploadReq)
            .pipe(
                filter(e => e.type === HttpEventType.Response),
                map(e => (e as HttpResponse<QuotationAttachmentUploadFileModel>).body),
                map(e => new QuotationAttachmentUploadFileModel(e.fileName, e.originalFileName))
            );
    }
    
    deleteQuotation(id: number) {
        return this._http.delete<void>(`${this._baseUrl}/quotation/${id}`)
            .pipe(
                map(() => { })
            );
    }
    
}
