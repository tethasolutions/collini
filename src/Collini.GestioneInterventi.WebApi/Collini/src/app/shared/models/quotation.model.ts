import { JobModel } from './job.model';
import { NoteModel } from './note.model';
import { QuotationStatusEnum } from '../enums/quotation-status.enum';

export class QuotationModel {
    id: number;
    amount: number;
    expirationDate: Date;
    status: QuotationStatusEnum;
    jobId: number;
    customerName: string;
    jobCode: string;
    jobDescription: string;
}
