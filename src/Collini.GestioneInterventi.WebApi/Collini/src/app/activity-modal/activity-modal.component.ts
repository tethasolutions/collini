import { Component, ViewChild } from '@angular/core';
import { ActivityModel } from '../shared/models/activity.model';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { MessageBoxService } from '../services/common/message-box.service';
import { markAsDirty } from '../services/common/functions';
import { JobOperatorModel } from '../shared/models/job-operator.model';
import { JobsService } from '../services/jobs.service';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { ActivityStatusEnum } from '../shared/enums/activity-status.enum';
import { SimpleLookupModel } from '../shared/models/simple-lookup.model';
import { JobModel } from '../shared/models/job.model';

@Component({
  selector: 'app-activity-modal',
  templateUrl: './activity-modal.component.html',
  styleUrls: ['./activity-modal.component.scss']
})
export class ActivityModalComponent extends ModalComponent<ActivityModel> {

    @ViewChild('form') form: NgForm;
    readonly role = Role;

    operators: Array<JobOperatorModel> = [];
    jobs: Array<JobModel> = [];
    states: Array<SimpleLookupModel> = [];

    constructor(
        private readonly _messageBox: MessageBoxService,
        private readonly _jobsService: JobsService
    ) {
        super();
    }

    protected _canClose() {
        markAsDirty(this.form);

        if (this.form.invalid) {
            this._messageBox.error('Compilare correttamente tutti i campi');
        }

        return this.form.valid;
    }

    protected _readOperators() {
        this._subscriptions.push(
          this._jobsService.getOperators()
            .pipe(
                tap(e => {
                  this.operators = e;
                })
            )
            .subscribe()
        );
    }

    protected _readjobs() {
        this._subscriptions.push(
          this._jobsService.getAllJobs()
            .pipe(
                tap(e => {
                  this.jobs = e;
                })
            )
            .subscribe()
        );
    }

    setStates() {
        this.states = [];
        for(var n in ActivityStatusEnum) {
            if (typeof ActivityStatusEnum[n] === 'number') {
              this.states.push({id: <any>ActivityStatusEnum[n], name: n});
            }
        }
    }

    public loadData() {
        this._readOperators();
        this._readjobs();
        this.setStates();
    }
}
