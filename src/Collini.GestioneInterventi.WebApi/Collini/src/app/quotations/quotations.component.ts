import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { QuotationsService } from '../services/quotations.service';
import { AddressesService } from '../services/addresses.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { Router, NavigationEnd } from '@angular/router';
import { QuotationStatusEnum } from '../shared/enums/quotation-status.enum';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { QuotationModalComponent } from '../quotation-modal/quotation-modal.component';
import { QuotationDetailModel } from '../shared/models/quotation-detail.model';

@Component({
  selector: 'app-quotations',
  templateUrl: './quotations.component.html',
  styleUrls: ['./quotations.component.scss']
})
export class QuotationsComponent extends BaseComponent implements OnInit {

  @ViewChild('quotationModal', { static: true }) quotationModal: QuotationModalComponent;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;

  quotationNotes: Array<NoteModel> = [];
  
  dataQuotations: GridDataResult;
  stateGridQuotations: State = {
      skip: 0,
      take: 10,
      filter: {
          filters: [],
          logic: 'and'
      },
      group: [],
      sort: []
  };

  constructor(
      private readonly _quotationsService: QuotationsService,
      private readonly _notesService: NotesService,
      private readonly _messageBox: MessageBoxService,
      private readonly _router: Router
  ) {
      super();
  }

  ngOnInit() {
      console.log(this._router.url);
      this._readQuotations();
  }

  dataStateChange(state: State) {
      this.stateGridQuotations = state;
      this._readQuotations();
  }

  protected _readQuotations() {
    this._subscriptions.push(
      this._quotationsService.readQuotations(this.stateGridQuotations)
        .pipe(
            tap(e => {
              console.log(e);
              this.dataQuotations = e;
            })
        )
        .subscribe()
    );
  }

  createQuotation() {
    const request = new QuotationDetailModel();
    this.quotationModal.loadData();
    this._subscriptions.push(
      this.quotationModal.open(request)
          .pipe(
              filter(e => e),
              switchMap(() => this._quotationsService.createQuotation(request)),
              tap(e => {
                this._messageBox.success(`Preventivo creato`);
              }),
              tap(() => this._readQuotations())
          )
          .subscribe()
    );
  }

  editQuotation(quotation: QuotationDetailModel) {
    this.quotationModal.loadData();
    this._subscriptions.push(
      this._quotationsService.getQuotationDetail(quotation.id)
        .pipe(
            map(e => {
              return e;
            }),
            switchMap(e => this.quotationModal.open(e)),
            filter(e => e),
            map(() => this.quotationModal.options),
            switchMap(e => this._quotationsService.updateQuotation(e, e.id)),
            map(() => this.quotationModal.options),
            tap(e => this._messageBox.success(`Preventivo aggiornato`)),
            tap(() => this._readQuotations())
        )
      .subscribe()
    );
  }

  viewNotes(quotation: QuotationDetailModel) {
    this.notesModal.id = quotation.id;
    this.notesModal.loadData();
    this.notesModal.open(null);
    /* this._subscriptions.push(
      this._notesService.getQuotationNotes(quotation.id)
        .pipe(
            map(e => {
              this.quotationNotes = e;
            }),
            switchMap(e => this.notesModal.open(e))
        )
      .subscribe()
    ); */
  }
}