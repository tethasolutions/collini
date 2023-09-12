import { JobModel } from './job.model';
import { ContactModel } from './contact.model';
import { NoteModel } from './note.model';
import { OrderStatusEnum } from '../enums/order-status.enum';

export class OrderModel {
    id: number;
    code: string;
    description: string;
    createdOn: Date;
    expirationDate: Date;
    status: OrderStatusEnum;
    jobId: number;
    customerName: string;
    jobCode: string;
    jobDate: Date;
    jobDescription: string;
    supplierId: number;
    supplier: ContactModel;
    
    get expired(): boolean {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const expiration = new Date(this.expirationDate);
        expiration.setHours(0, 0, 0, 0);

        return today > expiration;
    }
    
}