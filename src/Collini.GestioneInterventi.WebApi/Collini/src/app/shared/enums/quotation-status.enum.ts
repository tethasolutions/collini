import { Dictionary } from "src/app/services/common/models";

export enum QuotationStatusEnum {
    Pending,
    Sent,
    Accepted,
    Refused,
    Canceled
}


export const quotationStatusNames: Dictionary<QuotationStatusEnum, string> = {

    [QuotationStatusEnum.Accepted]: 'Accettato',
    [QuotationStatusEnum.Canceled]: 'Annullato',
    [QuotationStatusEnum.Pending]: 'In Attesa',
    [QuotationStatusEnum.Refused]: 'Rifiutato',
    [QuotationStatusEnum.Sent]: 'Consegnato'

};