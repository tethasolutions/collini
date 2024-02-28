import { Component, Input } from '@angular/core';
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

@Component({
  selector: 'app-job-activities-modal',
  templateUrl: './job-activities-modal.component.html'
})
export class JobActivitiesModalComponent extends ModalComponent<JobActivitiesModel> {

  constructor(
    private readonly _messageBox: MessageBoxService, 
    private readonly _jobService: JobsService) {
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
}
