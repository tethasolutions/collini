import { Component, Input, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { Role } from '../services/security/models';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { NoteAttachmentModel } from '../shared/models/note-attachment.model';
import { JobActivitiesModel } from '../shared/models/job-detail.model';
import { JobsService } from '../services/jobs.service';
import { Observable } from 'rxjs';
import { ActivityModalComponent } from '../activity-modal/activity-modal.component';
import { ActivityModel } from '../shared/models/activity.model';
import { ActivitiesService } from '../services/activities.service';

@Component({
  selector: 'app-job-activities-modal',
  templateUrl: './job-activities-modal.component.html'
})
export class JobActivitiesModalComponent extends ModalComponent<JobActivitiesModel> {

  @ViewChild('activityModal', { static: true }) activityModal: ActivityModalComponent;

  constructor(
    private readonly _messageBox: MessageBoxService, 
    private readonly _activitiesService: ActivitiesService) {
    super();
    this.options = new JobActivitiesModel();
  }

  override open(options: JobActivitiesModel): Observable<boolean> {
    const result = super.open(options);

    return result;
  }

  protected _canClose() {
    return true;
  }
  
  editActivity(activity: ActivityModel) {
    this.activityModal.loadData();
    this._subscriptions.push(
      this._activitiesService.getActivity(activity.id)
        .pipe(
            map(e => {
              return e;
            }),
            switchMap(e => this.activityModal.open(e)),
            filter(e => e),
            map(() => this.activityModal.options),
            switchMap(e => this._activitiesService.updateActivity(e, e.id)),
            map(() => this.activityModal.options),
            tap(e => this._messageBox.success(`Intervento aggiornato`))
        )
      .subscribe()
    );
  }
}
