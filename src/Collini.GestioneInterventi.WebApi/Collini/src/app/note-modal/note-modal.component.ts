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
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { NoteAttachmentsModalComponent } from '../note-attachments-modal/note-attachments-modal.component';

@Component({
  selector: 'app-note-modal',
  templateUrl: './note-modal.component.html',
  styleUrls: ['./note-modal.component.scss']
})
export class NoteModalComponent extends ModalComponent<NoteModel> {

    @ViewChild('form') form: NgForm;
    @ViewChild('notesAtachmentsModal', { static: true }) notesAtachmentsModal: NoteAttachmentsModalComponent;

    operators: Array<JobOperatorModel> = [];

    constructor(
        private readonly _messageBox: MessageBoxService,
        private readonly _jobsService: JobsService,
        private readonly _notesService: NotesService
    ) {
        super();
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

    public loadData() {
        this._readOperators();
    }

    protected _canClose() {
      markAsDirty(this.form);

      if (this.form.invalid) {
          this._messageBox.error('Compilare correttamente tutti i campi');
      }

      return this.form.valid;
    }

    viewAttachments() {
      this.notesAtachmentsModal.id = this.options.id;
      this.notesAtachmentsModal.loadData();
      this.notesAtachmentsModal.open(null);
    }
}
