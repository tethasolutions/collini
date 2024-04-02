import { Pipe, PipeTransform } from '@angular/core';
import { BaseComponent } from '../shared/base.component';
import { ActivityStatusEnum } from '../shared/enums/activity-status.enum';
import { compileClassMetadata } from '@angular/compiler';

@Pipe({
    name: 'activityStatus'
})
export class ActivityStatusPipe extends BaseComponent implements PipeTransform {

    transform(value: ActivityStatusEnum) {
        switch (value) {
            case ActivityStatusEnum.Planned:
                return 'Pianificato';
            case ActivityStatusEnum.CompletedSuccessfully:
                return 'Completato OK';
            case ActivityStatusEnum.CompletedUnsuccessfully:
                return 'Completato KO';
            case ActivityStatusEnum.Canceled:
                return "Annullato";
            case ActivityStatusEnum.ToComplete:
                return "Da Terminare";
            case ActivityStatusEnum.CompletedQuotation:
                return "Completato OK Prev";
            case ActivityStatusEnum.MaterialReady:
                return "Pianificato Materiale Pronto"
            default:
                return value;
        }
    }

}

