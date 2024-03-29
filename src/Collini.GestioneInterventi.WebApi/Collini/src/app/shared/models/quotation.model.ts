import { JobModel } from './job.model';
import { NoteModel } from './note.model';
import { QuotationStatusEnum } from '../enums/quotation-status.enum';

export class QuotationModel {
    id: number;
    amount: number;
    createdOn: Date;
    expirationDate: Date;
    status: QuotationStatusEnum;
    jobId: number;
    customerName: string;
    customerContacts: string;
    jobCode: string;
    jobDate: Date;
    jobDescription: string;
    activityStart: Date;
    activityEnd: Date;
    activityOperator: string;
    
    get expired(): boolean {
        const today = new Date();
        today.setHours(0, 0, 0, 0);

        const expiration = new Date(this.expirationDate);
        expiration.setHours(0, 0, 0, 0);

        return today > expiration;
    }
    
    
}
