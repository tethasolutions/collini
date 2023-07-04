import { Component, ViewChild, Input } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { AddressModel } from '../shared/models/address.model';
import { NgForm } from '@angular/forms';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { Role } from '../services/security/models';
import { NoteModel } from '../shared/models/note.model';
import { NoteModalComponent } from '../note-modal/note-modal.component';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { NotesService } from '../services/notes.service';
import { NoteAttachmentsModalComponent } from '../note-attachments-modal/note-attachments-modal.component';

@Component({
  selector: 'app-notes-modal',
  templateUrl: './notes-modal.component.html',
  styleUrls: ['./notes-modal.component.scss']
})

export class NotesModalComponent extends ModalComponent<any> {

    // @Input() note: Array<NoteModel> = [];

    
    @Input() notesType: string = null;
    public id: number = null;

    note: Array<NoteModel> = [];

    @ViewChild('noteModal', { static: true }) noteModal: NoteModalComponent;
    @ViewChild('notesAtachmentsModal', { static: true }) notesAtachmentsModal: NoteAttachmentsModalComponent;

    constructor(private readonly _messageBox: MessageBoxService, private readonly _notesService: NotesService) {
        super();
    }

    aggiungiNota() {
        const request = new NoteModel();
        if (this.notesType == 'activity') { request.activityId = this.id; }
        if (this.notesType == 'job') { request.jobId = this.id; }
        if (this.notesType == 'quotation') { request.quotationId = this.id; }
        this.noteModal.loadData();
        this._subscriptions.push(
            this.noteModal.open(request)
                .pipe(
                    filter(e => e),
                    switchMap(() => this._notesService.createNote(request)),
                    tap(e => {
                      this._messageBox.success(`Nota creata`);
                    }),
                    tap(() => {
                      this.loadData();
                    })
                )
                .subscribe()
        );
    }

    modificaNota(nota: NoteModel) {
        this.noteModal.loadData();
        this._subscriptions.push(
          this._notesService.getNoteDetail(nota.id)
            .pipe(
                map(e => {
                  return e;
                }),
                switchMap(e => this.noteModal.open(e)),
                filter(e => e),
                map(() => this.noteModal.options),
                switchMap(e => this._notesService.updateNote(e, e.id)),
                map(() => this.noteModal.options),
                tap(e => this._messageBox.success(`Nota aggiornata`)),
                tap(() => this.loadData())
            )
          .subscribe()
        );
    }

    protected _readActivityNotes() {
        this._subscriptions.push(
          this._notesService.getActivityNotes(this.id)
            .pipe(
                tap(e => {
                  this.note = e;
                })
            )
            .subscribe()
        );
    }

    protected _readJobNotes() {
        this._subscriptions.push(
          this._notesService.getJobNotes(this.id)
            .pipe(
                tap(e => {
                  this.note = e;
                })
            )
            .subscribe()
        );
    }
  
    protected _readQuotationNotes() {
      this._subscriptions.push(
        this._notesService.getQuotationNotes(this.id)
          .pipe(
              tap(e => {
                this.note = e;
              })
          )
          .subscribe()
      );
  }
  
  protected _readOrderNotes() {
    this._subscriptions.push(
      this._notesService.getOrderNotes(this.id)
        .pipe(
            tap(e => {
              this.note = e;
            })
        )
        .subscribe()
    );
}
    public loadData() {
        if (this.notesType == 'activity') {
            this._readActivityNotes();
        }
        if (this.notesType == 'job') {
            this._readJobNotes();
        }
        if (this.notesType == 'quotation') {
            this._readQuotationNotes();
        }
        if (this.notesType == 'order') {
            this._readOrderNotes();
        }
    }

    viewNoteAttachments(nota: NoteModel) {
      this.notesAtachmentsModal.id = nota.id;
      this.notesAtachmentsModal.loadData();
      this.notesAtachmentsModal.open(null);
    }

    protected _canClose() {
        return true;
    }
}
