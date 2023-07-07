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
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NoteAttachmentModel } from '../shared/models/note-attachment.model';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-note-attachment-modal',
  templateUrl: './note-attachment-modal.component.html',
  styleUrls: ['./note-attachment-modal.component.scss']
})
export class NoteAttachmentModalComponent extends ModalComponent<NoteAttachmentModel> {
  
  @ViewChild('form') form: NgForm;

  constructor(
      private readonly _messageBox: MessageBoxService,
      private readonly _jobsService: JobsService,
      private readonly _notesService: NotesService,
      private readonly _http: HttpClient
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

  fileName = '';


  onFileSelected(event: { target: { files: File[]; }; }) {

      const file:File = event.target.files[0];

      if (file) 
      {       
          const formData = new FormData();
          formData.append("file", file);
          const upload$ = this._http.post("/api/notes/note-attachment", formData);
          upload$.subscribe();
          this.options.fileName = file.name;
      }
  }

}
