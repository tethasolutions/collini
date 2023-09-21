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

import { RemoveEvent, SuccessEvent, FileInfo,FileState,SelectEvent} from "@progress/kendo-angular-upload";
import { QuotationAttachmentUploadFileModel } from '../shared/models/quotation-attachment-upload-file.model';
import { Observable } from 'rxjs';
import { NoteAttachmentUploadFileModel } from '../shared/models/note-attachment-upload-file.model';

@Component({
  selector: 'app-note-modal',
  templateUrl: './note-modal.component.html',
  styleUrls: ['./note-modal.component.scss']
})
export class NoteModalComponent extends ModalComponent<NoteModel> {

    @ViewChild('form') form: NgForm;
    
    @ViewChild('noteAttachmentModal', { static: true }) noteAttachmentModal: NoteAttachmentModalComponent;

    allegati: Array<NoteAttachmentModel> = [];
    
    operators: Array<JobOperatorModel> = [];

    isUploaded:Array<boolean>= [];
    attachmentsFileInfo:Array<FileInfo>= [];
    attachmentsUploads: Array<NoteAttachmentUploadFileModel> =[];
    private readonly _baseUrl = `${ApiUrls.baseApiUrl}/notes`;
    uploadSaveUrl = `${this._baseUrl}/note-attachment/upload-file`;
    uploadRemoveUrl = `${this._baseUrl}/note-attachment/remove-file`; 
    imageExtensions:Array<string>= ['.jpg','.png','.jpeg','.gif','.bmp','.webp'];
    

    constructor(
        private readonly _messageBox: MessageBoxService,
        private readonly _jobsService: JobsService,
        private readonly _notesService: NotesService
    ) {
        super();
    }

    override open(options: NoteModel): Observable<boolean> 
    {
      const result = super.open(options);
      this.attachmentsFileInfo = [];
      this.isUploaded = [];
      this.attachmentsUploads = [];
      this.allegati = this.options.attachments;
      
      this.options.attachments.forEach(element => {
        if(element.displayName !=null && element.fileName != null)
        {
          const noteAttachment = new NoteAttachmentUploadFileModel(element.fileName,element.displayName);
          this.attachmentsUploads.push(noteAttachment);
          this.attachmentsFileInfo.push({name: element.displayName});  
          this.isUploaded.push(true);
        }
      });    
      return result;
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

    public CreateUrl(fileName:string) : string
    {
      let ret = "";
      this.attachmentsUploads.forEach(element => {
        if(element.originalFileName == fileName)
        ret = `${this._baseUrl}/note-attachment/download-file/${element.fileName}/${element.originalFileName}`;
       });       
       return ret;
    }
   
    public AttachmentExecutionSuccess(e: SuccessEvent): void
    {
      const body = e.response.body;
      if(body != null)
      {

        const uploadedFile = body as QuotationAttachmentUploadFileModel;
        const noteAttachment = new NoteAttachmentUploadFileModel(uploadedFile.fileName,uploadedFile.originalFileName);
        this.attachmentsUploads.push(noteAttachment);        
        let noteAttachmentModal = new NoteAttachmentModel();
        noteAttachmentModal.fileName = uploadedFile.fileName;
        noteAttachmentModal.displayName = uploadedFile.originalFileName;
        this.options.attachments.push(noteAttachmentModal);        
        this.isUploaded.push(true);
      }
      else
      {
        const deletedFile = e.files[0].name;
        const index = this.attachmentsUploads.findIndex(x=>x.originalFileName == deletedFile);
        if(index>-1)
        {
        this.attachmentsUploads.splice(index,1);
        this.options.attachments.splice(index,1);        
        this.isUploaded.pop();
        }
      }
    }

    public AttachmentSelect(e: SelectEvent): void
    {
      const files = e.files;
      let popup = false;
      files.forEach(element => {
        var index = this.attachmentsUploads.findIndex(x=>x.originalFileName == element.name);
        if(index > -1)
        {
          files.splice(index,1);
        popup = true;
        }
      });     
      if(popup)
      {
        this._messageBox.alert(`Sono presenti tra i file caricati alcuni file con lo stesso nome di quelli che si vogliono caricare`);
      }
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
