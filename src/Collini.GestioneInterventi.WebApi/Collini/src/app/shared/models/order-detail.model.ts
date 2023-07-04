import { JobSourceModel } from './job-source.model';
import { OrderStatusEnum } from '../enums/order-status.enum';
import { ContactModel } from './contact.model';

export class OrderDetailModel {
    id: number;
    code: string;
    description: string;
    expirationDate: Date;
    sourceId: number;
    source: JobSourceModel;
    supplierId: number;
    supplier: ContactModel;
    status: OrderStatusEnum;
    jobId: number;
    customerName: string;
    jobCode: string;
    jobDescription: string;

    constructor() {
        this.id = null;
        this.code = null;
        this.description = null;
        this.expirationDate = new Date((new Date()).getTime() + (1000 * 60 * 60 * 24 * 2));
        this.sourceId = null;
        this.source = new JobSourceModel();
        this.supplierId = null;
        this.supplier = new ContactModel();
        this.status = OrderStatusEnum.Pending;
    }
}