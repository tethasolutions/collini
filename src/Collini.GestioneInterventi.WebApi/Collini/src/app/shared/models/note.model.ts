import { JobModel } from './job.model';
import { OrderModel } from './order.model';
import { NoteAttachmentModel } from './note-attachment.model';
import { QuotationModel } from './quotation.model';
import { ActivityModel } from './activity.model';
import { CustomerModel } from './customer.model';

export class NoteModel {
    id: number;
    value: string;
    /* jobId: number;
    job: JobModel;
    orderId: number;
    order: OrderModel;
    quotationId: number;
    quotation: QuotationModel;
    activityId: number;
    activity: ActivityModel; */

    createdOn: Date;
    operator: CustomerModel;
    type: string;

    attachments: NoteAttachmentModel[];

    constructor() {
        this.id = null;
        this.value = null;
        this.createdOn = new Date();
        this.operator = null;
        this.type = null;
        this.attachments = [];
    }
}
