import { Component, ViewChild } from '@angular/core';
import { ActivityModel } from '../shared/models/activity.model';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { MessageBoxService } from '../services/common/message-box.service';
import { JobOperatorModel } from '../shared/models/job-operator.model';
import { JobsService } from '../services/jobs.service';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { CopyActivityModel } from '../shared/models/copy-activity.model';
import { ActivitiesService } from '../services/activities.service';
import { markAsDirty } from '../services/common/functions';

@Component({
  selector: 'app-copy-activity-modal',
  templateUrl: './copy-activity-modal.component.html'
})
export class CopyActivityModalComponent extends ModalComponent<ActivityModel> {

    @ViewChild('form') form: NgForm;
    readonly role = Role;
    public id: number = null;

    operators: Array<JobOperatorModel> = [];
    operatorId: number;
    start: Date;
    end: Date;

    constructor(
        private readonly _messageBox: MessageBoxService,
        private readonly _jobsService: JobsService,
        private readonly _activityService: ActivitiesService
    ) {
        super();
    }

    override open(options: ActivityModel): Observable<boolean>
    {
      const result = super.open(options);

      this.loadData()
      this.operatorId = null;
      this.start = options.start;
      this.end = options.end;
      return result;
    }
    
    override close(){
      if (this.form.invalid){
        markAsDirty(this.form);
        return;
      }
      const request = new CopyActivityModel();
      request.id = this.options.id;
      request.newOperatorId = this.operatorId;
      request.start = this.start;
      request.end = this.end;
      this._subscriptions.push(
        this._activityService.copyActivity(request)
        .pipe(
          tap(()=> super.close())
        )
        .subscribe()
      );
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

    public loadData() 
    {
      this._readOperators();
    }

}
