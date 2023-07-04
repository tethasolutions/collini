import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { listEnum, markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { SimpleLookupModel } from '../shared/models/simple-lookup.model';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NoteModel } from '../shared/models/note.model';
import { NotesService } from '../services/notes.service';
import { QuotationDetailModel } from '../shared/models/quotation-detail.model';
import { QuotationsService } from '../services/quotations.service';
import { QuotationStatusEnum } from '../shared/enums/quotation-status.enum';

@Component({
  selector: 'app-quotation-modal',
  templateUrl: './quotation-modal.component.html',
  styleUrls: ['./quotation-modal.component.scss']
})
export class QuotationModalComponent extends ModalComponent<QuotationDetailModel> {

  @ViewChild('form') form: NgForm;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
  readonly role = Role;
  name = '';

  states = listEnum<QuotationStatusEnum>(QuotationStatusEnum);
  quotationNotes: Array<NoteModel> = [];

  constructor(private readonly _messageBox: MessageBoxService, 
              private readonly _quotationsService: QuotationsService,
              private readonly _notesService: NotesService) {
    super();
    this.options = new QuotationDetailModel();
  }

  protected _canClose() {
      markAsDirty(this.form);

      if (this.form.invalid) {
          this._messageBox.error('Compilare correttamente tutti i campi');
      }

      return this.form.valid;
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

  public loadData() {
  }
}
