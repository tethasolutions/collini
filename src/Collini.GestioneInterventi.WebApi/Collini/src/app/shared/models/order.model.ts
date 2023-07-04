import { JobModel } from './job.model';
import { ContactModel } from './contact.model';
import { NoteModel } from './note.model';
import { OrderStatusEnum } from '../enums/order-status.enum';

export class OrderModel {
    id: number;
    code: string;
    description: string;
    expirationDate: Date;
    status: OrderStatusEnum;
    jobId: number;
    customerName: string;
    jobCode: string;
    jobDescription: string;
    supplierId: number;
    supplier: ContactModel;
}