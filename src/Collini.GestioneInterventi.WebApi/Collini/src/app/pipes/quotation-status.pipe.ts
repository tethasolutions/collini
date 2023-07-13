import { Pipe, PipeTransform } from '@angular/core';
import { BaseComponent } from '../shared/base.component';
import { QuotationStatusEnum } from '../shared/enums/quotation-status.enum';
import { compileClassMetadata } from '@angular/compiler';

@Pipe({
    name: 'quotationStatus'
})
export class QuotationStatusPipe extends BaseComponent implements PipeTransform {

    transform(value: QuotationStatusEnum) {
        switch (value) {
            case QuotationStatusEnum.Pending:
                return 'In Attesa';
            case QuotationStatusEnum.Sent:
                return 'Consegnato';
            case QuotationStatusEnum.Accepted:
                return 'Accettato';
            case QuotationStatusEnum.Refused:
                return 'Rifiutato';
            case QuotationStatusEnum.Canceled:
                return "Annullato";
            default:
                return value;
        }
    }

}

