import { JobModel } from './job.model';
import { OrderModel } from './order.model';
import { NoteAttachmentModel } from './note-attachment.model';
import { QuotationModel } from './quotation.model';
import { ActivityModel } from './activity.model';

export class NoteModel {
    value: string;
    jobId: number;
    job: JobModel;
    orderId: number;
    order: OrderModel;
    quotationId: number;
    quotation: QuotationModel;
    activityId: number;
    activity: ActivityModel;
    attachments: NoteAttachmentModel[];
}