import { QuotationModel } from "./quotation.model";


export class QuotationAttachmentModel {
    id: number;
    displayName: string;
    fileName: string;
    quotationId: number;
    quotation: QuotationModel;

    constructor() {
        this.id = null;
        this.displayName = null;
        this.fileName = null;
        this.quotationId = null;
        this.quotation = new QuotationModel();
    }
}
