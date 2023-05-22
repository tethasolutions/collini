import { ContactAddressModel } from './contact-address.model';
import { OrderModel } from './order.model';
import { ContactModel } from './contact.model';
import { NoteModel } from './note.model';
import { ActivityModel } from './activity.model';
import { QuotationModel } from './quotation.model';
import { ProductTypeModel } from './product-type.model';
import { JobSourceModel } from './job-source.model';
import { JobStatusEnum } from '../enums/job-status.enum';

export class JobModel {
    number: number;
    year: number;
    expirationDate: Date;
    description: string;
    status: JobStatusEnum;
    statusChangedOn: Date;

    customerId: number;
    customer: ContactModel;

    customerAddressId: number;
    customerAddress: ContactAddressModel;

    sourceId: number;
    source: JobSourceModel;

    productTypeId: number;
    productType: ProductTypeModel;

    notes: NoteModel[];
    quotations: QuotationModel[];
    orders: OrderModel[];
    activities: ActivityModel[];
}