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
import { NoteAttachmentModel } from '../shared/models/note-attachment.model';
import { NoteAttachmentModalComponent } from '../note-attachment-modal/note-attachment-modal.component';
import { ApiUrls } from '../services/common/api-urls';

@Component({
  selector: 'app-note-modal',
  templateUrl: './note-modal.component.html',
  styleUrls: ['./note-modal.component.scss']
})
export class NoteModalComponent extends ModalComponent<NoteModel> {

    @ViewChild('form') form: NgForm;
    
    @ViewChild('noteAttachmentModal', { static: true }) noteAttachmentModal: NoteAttachmentModalComponent;

    allegati: Array<NoteAttachmentModel> = [];
    readonly baseUrl = `${ApiUrls.baseUrl}/attachments/`;
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
        if(this.options != null)
        {
        this._readNoteAttachments()
        }
    }

    protected _canClose() {
      markAsDirty(this.form);

      if (this.form.invalid) {
          this._messageBox.error('Compilare correttamente tutti i campi');
      }

      return this.form.valid;
    }

    // viewAttachments() {
    //   this.notesAttachmentsModal.id = this.options.id;
    //   this.notesAttachmentsModal.loadData();
    //   this.notesAttachmentsModal.open(null);
    // }

    aggiungiAllegato() {
      const request = new NoteAttachmentModel();
      request.noteId = this.options.id;
      this._subscriptions.push(
          this.noteAttachmentModal.open(request)
              .pipe(
                  filter(e => e),
                  switchMap(() => this._notesService.createNoteAttachment(request)),
                  tap(e => {
                    this._messageBox.success(`Allegato creato`);
                  }),
                  tap(() => {
                    this.loadData();
                  })
              )
              .subscribe()
      );
    }
  
    modificaAllegato(allegato: NoteAttachmentModel) {
      this._subscriptions.push(
        this._notesService.getNoteAttachmentDetail(allegato.id)
          .pipe(
              map(e => {
                return e;
              }),
              switchMap(e => this.noteAttachmentModal.open(e)),
              filter(e => e),
              map(() => this.noteAttachmentModal.options),
              switchMap(e => this._notesService.updateNoteAttachment(e, e.id)),
              map(() => this.noteAttachmentModal.options),
              tap(e => this._messageBox.success(`Allegato aggiornato`)),
              tap(() => this.loadData())
          )
        .subscribe()
      );
    }
  
    protected _readNoteAttachments() {
      this._subscriptions.push(
        this._notesService.getNoteAttachments(this.options.id)
          .pipe(
              tap(e => {
                this.allegati = e;
              })
          )
          .subscribe()
      );
    }
  

}
