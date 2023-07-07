import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { JobDetailModel } from '../shared/models/job-detail.model';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { listEnum, markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { JobsService } from '../services/jobs.service';
import { AddressModel } from '../shared/models/address.model';
import { CustomerModel } from '../shared/models/customer.model';
import { JobSourceModel } from '../shared/models/job-source.model';
import { ProductTypeModel } from '../shared/models/product-type.model';
import { JobOperatorModel } from '../shared/models/job-operator.model';
import { SimpleLookupModel } from '../shared/models/simple-lookup.model';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { JobStatusEnum } from '../shared/enums/job-status.enum';
import { AddressModalComponent } from '../address-modal/address-modal.component';
import { CustomerModalComponent } from '../customer-modal/customer-modal.component';
import { CustomerService } from '../services/customer.service';
import { AddressesService } from '../services/addresses.service';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NoteModel } from '../shared/models/note.model';
import { NotesService } from '../services/notes.service';

@Component({
  selector: 'app-job-modal',
  templateUrl: './job-modal.component.html',
  styleUrls: ['./job-modal.component.scss']
})
export class JobModalComponent extends ModalComponent<JobDetailModel> {

  @ViewChild('form') form: NgForm;
  @ViewChild('customerModal', { static: true }) customerModal: CustomerModalComponent;
  @ViewChild('addressModal', { static: true }) addressModal: AddressModalComponent;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
  readonly role = Role;
  name = '';

  operators: Array<JobOperatorModel> = [];
  customers: Array<CustomerModel> = [];
  sources: Array<JobSourceModel> = [];
  productTypes: Array<ProductTypeModel> = [];
  states = listEnum<JobStatusEnum>(JobStatusEnum);
  jobNotes: Array<NoteModel> = [];

  constructor(private readonly _messageBox: MessageBoxService,
              private readonly _jobsService: JobsService,
              private readonly _addressesService: AddressesService,
              private readonly _customerService: CustomerService,
              private readonly _notesService: NotesService) {
    super();
    this.options = new JobDetailModel();
  }

  protected _canClose() {
      markAsDirty(this.form);

      if (this.form.invalid) {
          this._messageBox.error('Compilare correttamente tutti i campi');
      }

      return this.form.valid;
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

  protected _readJobCustomers(creatoNuovoCustomer = false) {
    this._subscriptions.push(
      this._jobsService.getJobCustomers()
        .pipe(
            tap(e => {
              this.customers = e;
              if (creatoNuovoCustomer) {
                this.customerChanged(this.options.customerId);
              }
            })
        )
        .subscribe()
    );
  }

  protected _readJobSources() {
    this._subscriptions.push(
      this._jobsService.getJobSources()
        .pipe(
            tap(e => {
              this.sources = e;
            })
        )
        .subscribe()
    );
  }

  protected _readJobProductTypes() {
    this._subscriptions.push(
      this._jobsService.getJobProductTypes()
        .pipe(
            tap(e => {
              this.productTypes = e;
            })
        )
        .subscribe()
    );
  }

  customerChanged(customerId: number) {
    this.options.customerAddressId = null;
    if (customerId == undefined) {
      this.options.customer = new CustomerModel();
      return;
    }
    const nuovoCustomerSelezionato: CustomerModel = this.customers.find(x => x.id === customerId);
    if (nuovoCustomerSelezionato == undefined) { return; }
    this.options.customer = nuovoCustomerSelezionato;
  }

  createAddress() {
      const request = new AddressModel();
      request.contactId = this.options.id;
      this._subscriptions.push(
          this.addressModal.open(request)
              .pipe(
                  filter(e => e),
                  tap(() => {
                      this.addNewAddress(request);
                  })
              )
              .subscribe()
      );
  }

  addNewAddress(address: AddressModel) {
    this._subscriptions.push(
      this._addressesService.createAddress(address)
          .pipe(
              map(e => e),
              tap(e => {
                this.options.customerAddressId = e;
                address.id = e;
                const customerSelezionato: CustomerModel = this.customers.find(x => x.id === this.options.customerId);
                if (customerSelezionato != undefined) {
                  customerSelezionato.addresses.push(address);
                }
                this._messageBox.success(`Indirizzo creato con successo`)
              }),
              tap(() => {
                // this._readJobCustomers(true);
              })
          )
          .subscribe()
    );

    /* this.options.customer.addresses.push(address);
    if (address.isMainAddress) {
        this.options.customer.mainAddress = address;
        this.options.customerAddress = address;
        this.options.customer.addresses.forEach((item: AddressModel) => {
            item.isMainAddress = item.tempId === address.tempId;
        });
    } */
  }

  createCustomer() {
    const request = new CustomerModel();
    request.type = 0;

    this._subscriptions.push(
        this.customerModal.open(request)
            .pipe(
                filter(e => e),
                switchMap(() => this._customerService.createCustomer(request)),
                tap(e => {
                  this.options.customerId = e;
                  this._messageBox.success(`Cliente ${request.name} creato`);
                }),
                tap(() => this._readJobCustomers(true))
            )
            .subscribe()
    );
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
    this._readOperators();
    this._readJobCustomers();
    this._readJobSources();
    this._readJobProductTypes();
  }
}
