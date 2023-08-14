import { AddressModel } from './address.model';
import { CustomerModel } from './customer.model';
import { ProductTypeModel } from './product-type.model';
import { JobSourceModel } from './job-source.model';
import { JobStatusEnum } from '../enums/job-status.enum';

export class JobSearchModel {
    id: number;
    code:string;
    //description: string;
    resultNote: string;
    operatorId: number;
    jobDate: Date;
    expirationDate: Date;
    customerId: number;
    customer: CustomerModel;
    customerName: string;
    customerAddressId: number;
    customerAddress: AddressModel;
    customerFullAddress: string;
    sourceId: number;
    source: JobSourceModel;
    productTypeId: number;
    productType: ProductTypeModel;
    status: JobStatusEnum;
    lastDocument: string;
    lastDocumentDate: Date;
}
