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
import { OrderDetailModel } from '../shared/models/order-detail.model';
import { OrdersService } from '../services/orders.service';
import { OrderStatusEnum } from '../shared/enums/order-status.enum';
import { JobsService } from '../services/jobs.service';
import { CustomerModel } from '../shared/models/customer.model';
import { NoteModalComponent } from '../note-modal/note-modal.component';
import { JobModalComponent } from '../job-modal/job-modal.component';

@Component({
    selector: 'app-order-modal',
    templateUrl: './order-modal.component.html',
    styleUrls: ['./order-modal.component.scss']
})
export class OrderModalComponent extends ModalComponent<OrderDetailModel> {

    @ViewChild('form') form: NgForm;
    @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
    @ViewChild('noteModal', { static: true }) noteModal: NoteModalComponent;
    @ViewChild('jobModal', { static: true }) jobModal: JobModalComponent;
    readonly role = Role;
    name = '';

    states = listEnum<OrderStatusEnum>(OrderStatusEnum);
    orderNotes: Array<NoteModel> = [];
    suppliers: CustomerModel[] = [];

    constructor(private readonly _messageBox: MessageBoxService,
        private readonly _ordersService: OrdersService,
        private readonly _notesService: NotesService,
        private readonly _jobsService: JobsService) {
        super();
        this.options = new OrderDetailModel();
    }

    protected _canClose() {
        markAsDirty(this.form);

        if (this.form.invalid) {
            this._messageBox.error('Compilare correttamente tutti i campi');
        }

        return this.form.valid;
    }

    viewNotes() {
        // this.notesModal.id = this.options.code;
        // this.notesModal.loadData();
        // this.notesModal.open(null);
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

    customerChanged(event: any) {

    }

    createCustomer() {

    }

    public loadData() {
        this._jobsService.getJobSuppliers()
            .pipe(
                tap(e => this.suppliers = e)
            )
            .subscribe();
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
          tap(e => {
            this.options.jobDescription = e.description;
          })
        )
        .subscribe()
    );
  }
}
