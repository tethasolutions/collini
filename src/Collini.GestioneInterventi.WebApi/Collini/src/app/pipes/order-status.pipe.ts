import { Pipe, PipeTransform } from '@angular/core';
import { BaseComponent } from '../shared/base.component';
import { OrderStatusEnum } from '../shared/enums/order-status.enum';
import { compileClassMetadata } from '@angular/compiler';

@Pipe({
    name: 'orderStatus'
})
export class OrderStatusPipe extends BaseComponent implements PipeTransform {

    transform(value: OrderStatusEnum) {
        switch (value) {
            case OrderStatusEnum.Pending:
                return 'In Attesa';
            case OrderStatusEnum.Completed:
                return 'Completato';
            case OrderStatusEnum.Canceled:
                return "Annullato";
            default:
                return value;
        }
    }

}

