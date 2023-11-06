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
    status: OrderStatusEnum;
    jobId: number;
    customerName: string;
    supplierName: string;
    jobCode: string;
    jobDate: Date;
    jobDescription: string;
    hasNotes: boolean;

    constructor() {
        this.id = null;
        this.code = null;
        this.description = null;
        this.expirationDate = new Date((new Date()).getTime() + (1000 * 60 * 60 * 24 * 2));
        this.sourceId = null;
        this.source = new JobSourceModel();
        this.supplierId = null;
        this.status = OrderStatusEnum.Pending;
    }
}
