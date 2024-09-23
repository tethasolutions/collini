import { AddressModel } from './address.model';
import { CustomerModel } from './customer.model';
import { ProductTypeModel } from './product-type.model';
import { JobSourceModel } from './job-source.model';
import { JobStatusEnum } from '../enums/job-status.enum';
import { QuotationStatusEnum } from '../enums/quotation-status.enum';
import { OrderStatusEnum } from '../enums/order-status.enum';
import { ActivityStatusEnum } from '../enums/activity-status.enum';

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
    customerContact: string;
    sourceId: number;
    source: JobSourceModel;
    productTypeId: number;
    productType: ProductTypeModel;
    status: JobStatusEnum;
    lastQuotation: QuotationStatusEnum;
    lastQuotationDate: Date;
    lastOrder: OrderStatusEnum;
    lastOrderDate: Date;
    lastActivity: ActivityStatusEnum;
    lastActivityDate: Date;
    lastActivityOperator: string;
    lastOperatorColor: string;
}
