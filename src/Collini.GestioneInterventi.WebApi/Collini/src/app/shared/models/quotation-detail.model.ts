import { JobSourceModel } from './job-source.model';
import { QuotationStatusEnum } from '../enums/quotation-status.enum';

export class QuotationDetailModel {
    id: number;
    amount: number;
    expirationDate: Date;
    sourceId: number;
    source: JobSourceModel;
    status: QuotationStatusEnum;
    jobId: number;
    customerName: string;
    jobCode: string;
    jobDescription: string;

    constructor() {
        this.id = null;
        this.amount = 0;
        this.expirationDate = new Date((new Date()).getTime() + (1000 * 60 * 60 * 24 * 2));
        this.sourceId = null;
        this.source = new JobSourceModel();
        this.status = QuotationStatusEnum.Pending;
    }
}
