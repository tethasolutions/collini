import { Component, ViewChild, Input } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { AddressModel } from '../shared/models/address.model';
import { NgForm } from '@angular/forms';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { Role } from '../services/security/models';
import { NoteModel } from '../shared/models/note.model';
import { NoteAttachmentModalComponent } from '../note-attachment-modal/note-attachment-modal.component';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { NotesService } from '../services/notes.service';
import { NoteAttachmentModel } from '../shared/models/note-attachment.model';
import { ApiUrls } from '../services/common/api-urls';

@Component({
  selector: 'app-note-attachments-modal',
  templateUrl: './note-attachments-modal.component.html',
  styleUrls: ['./note-attachments-modal.component.scss']
})
export class NoteAttachmentsModalComponent extends ModalComponent<any> {

  public id: number = null;
  allegati: Array<NoteAttachmentModel> = [];
  @ViewChild('noteAttachmentModal', { static: true }) noteAttachmentModal: NoteAttachmentModalComponent;

  readonly baseUrl = `${ApiUrls.baseUrl}/attachments/`;

  constructor(private readonly _messageBox: MessageBoxService, private readonly _notesService: NotesService) {
    super();
  }

  aggiungiAllegato() {
    const request = new NoteAttachmentModel();
    request.noteId = this.id;
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
      this._notesService.getNoteAttachments(this.id)
        .pipe(
            tap(e => {
              this.allegati = e;
            })
        )
        .subscribe()
    );
  }

  public loadData() {
    this._readNoteAttachments();
  }

  protected _canClose() {
    return true;
  }
}
