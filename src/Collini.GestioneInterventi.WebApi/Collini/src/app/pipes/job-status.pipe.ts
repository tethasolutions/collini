import { Pipe, PipeTransform } from '@angular/core';
import { BaseComponent } from '../shared/base.component';
import { JobStatusEnum } from '../shared/enums/job-status.enum';
import { compileClassMetadata } from '@angular/compiler';

@Pipe({
    name: 'jobStatus'
})
export class JobStatusPipe extends BaseComponent implements PipeTransform {

    transform(value: JobStatusEnum) {
        switch (value) {
            case JobStatusEnum.Pending:
                return 'In Attesa';
            case JobStatusEnum.Working:
                return 'In Lavorazione';
            case JobStatusEnum.Billed:
                return 'Fatturato';
            case JobStatusEnum.Canceled:
                return "Annullato";
            case JobStatusEnum.Completed:
                return "Completato";
            default:
                return value;
        }
    }

}

