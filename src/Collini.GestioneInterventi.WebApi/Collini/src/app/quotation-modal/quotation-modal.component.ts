import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { listEnum, markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NoteModel } from '../shared/models/note.model';
import { NotesService } from '../services/notes.service';
import { QuotationDetailModel } from '../shared/models/quotation-detail.model';
import { QuotationsService } from '../services/quotations.service';
import { QuotationStatusEnum } from '../shared/enums/quotation-status.enum';
import { ApiUrls } from '../services/common/api-urls';
import { RemoveEvent, SuccessEvent, FileInfo,FileState } from "@progress/kendo-angular-upload";
import { QuotationAttachmentUploadFileModel } from '../shared/models/quotation-attachment-upload-file.model';
import { Observable, filter, map, switchMap, tap } from 'rxjs';
import { NoteModalComponent } from '../note-modal/note-modal.component';
import { JobModalComponent } from '../job-modal/job-modal.component';
import { JobsService } from '../services/jobs.service';

@Component({
  selector: 'app-quotation-modal',
  templateUrl: './quotation-modal.component.html',
  styleUrls: ['./quotation-modal.component.scss']
})
export class QuotationModalComponent extends ModalComponent<QuotationDetailModel> {

  @ViewChild('form') form: NgForm;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
  @ViewChild('noteModal', { static: true }) noteModal: NoteModalComponent;
  @ViewChild('jobModal', { static: true }) jobModal: JobModalComponent;
  
  readonly role = Role;
  name = '';
 
  isUploaded:boolean;
  
  attachments:Array<FileInfo>= [];

  private readonly _baseUrl = `${ApiUrls.baseApiUrl}/quotations`;
  uploadSaveUrl = `${this._baseUrl}/quotation-attachment/upload-file`;
  uploadRemoveUrl = `${this._baseUrl}/quotation-attachment/remove-file`; 

  states = listEnum<QuotationStatusEnum>(QuotationStatusEnum);
  quotationNotes: Array<NoteModel> = [];

  constructor(private readonly _messageBox: MessageBoxService, 
              private readonly _quotationsService: QuotationsService,
              private readonly _jobsService: JobsService,
              private readonly _notesService: NotesService) {
    super();
  this.options = new QuotationDetailModel();
   

  }

   override open(options: QuotationDetailModel): Observable<boolean> {
    
    const result = super.open(options);
    
    this.attachments = [];
    this.isUploaded = false;
    if(this.options.attachmentDisplayName != null && this.options.attachmentDisplayName != "")   
    {
      this.attachments = [{name: this.options.attachmentDisplayName}];
      this.isUploaded = true;
    }
    return result;
  }

 
  protected _canClose() {
      markAsDirty(this.form);

      if (this.form.invalid) {
          this._messageBox.error('Compilare correttamente tutti i campi');
      }

      return this.form.valid;
  }

  public CreateUrl () : string
  {
     return `${this._baseUrl}/quotation-attachment/download-file/${this.options.attachmentFileName}/${this.options.attachmentDisplayName}`;
  }
 
  public AttachmentExecutionSuccess(e: SuccessEvent): void
  {
    const body = e.response.body;
    if(body != null)
    {
      const uploadedFile = body as QuotationAttachmentUploadFileModel
      this.options.attachmentDisplayName = uploadedFile.originalFileName;
      this.options.attachmentFileName = uploadedFile.fileName;
      this.isUploaded = true;
    }
    else
    {
      this.options.attachmentDisplayName = "";
      this.options.attachmentFileName = "";
      this.isUploaded = false;
    }

  }
  viewNotes() {
    this.notesModal.id = this.options.id;
    this.notesModal.loadData();
    this.notesModal.open(null);
    /* this._subscriptions.push(
      this._notesService.getJobNotes(this.options.id)
        .pipe(
            map(e => {
              this.jobNotes = e;
            }),
            switchMap(e => this.notesModal.open(e))
        )
      .subscribe()
    ); */
  }

  viewLastNote() {     
    this.notesModal.id = this.options.jobId;
    this._subscriptions.push(
            this._notesService.getLastJobNote(this.options.jobId)
            .pipe(        
                switchMap(e => this.noteModal.open(e)),
                filter(e => e),
                map(() => this.noteModal.options),
                switchMap(e => this._notesService.updateNote(e, e.id)),
                map(() => this.noteModal.options),
                tap(e => this._messageBox.success(`Nota aggiornata`)),           
            )
    .subscribe()
    );
  }

  public loadData() {
  }
  
  editJob() {

    this._subscriptions.push(
      this._jobsService.getJobDetail(this.options.jobId)
        .pipe(
          map(e => {
            return e;
          }),
          switchMap(e => this.jobModal.open(e)),
          filter(e => e),
          map(() => this.jobModal.options),
          switchMap(e => this._jobsService.updateJob(e, e.id)),
          map(() => this.jobModal.options),
          tap(e => this._messageBox.success(`Job '${e.description}' aggiornato`)),
          tap(() => this.loadData())
        )
        .subscribe()
    );
  }
}


