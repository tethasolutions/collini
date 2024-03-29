import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { JobActivitiesModel, JobDetailModel } from '../shared/models/job-detail.model';
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
import { emitDistinctChangesOnlyDefaultValue } from '@angular/compiler';
import { Observable } from 'rxjs';
import { ComboBoxComponent } from '@progress/kendo-angular-dropdowns';
import { NoteModalComponent } from '../note-modal/note-modal.component';
import { JobActivitiesModalComponent } from '../job-activities-modal/job-activities-modal.component';

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
  @ViewChild('noteModal', { static: true }) noteModal: NoteModalComponent;
  @ViewChild('jobActivitiesModal', { static: true }) jobActivitiesModal: JobActivitiesModalComponent;
  readonly role = Role;
  name = '';

  operators: Array<JobOperatorModel> = [];

  customers: Array<CustomerModel> = [];
  customersFiltered: Array<CustomerModel> = [];
  customerAddresses: Array<AddressModel> = [];

  sources: Array<JobSourceModel> = [];
  productTypes: Array<ProductTypeModel> = [];
  states = listEnum<JobStatusEnum>(JobStatusEnum);
  jobNotes: Array<NoteModel> = [];
  jobActivities: JobActivitiesModel = null;

  readonly jobStatusEnum = JobStatusEnum;

  addressCombo: ComboBoxComponent;
  customerSelezionato = new CustomerModel();

  constructor(private readonly _messageBox: MessageBoxService,
    private readonly _jobsService: JobsService,
    private readonly _addressesService: AddressesService,
    private readonly _customerService: CustomerService,
    private readonly _notesService: NotesService) {
    super();
    this.options = new JobDetailModel();
  }

  override open(options: JobDetailModel): Observable<boolean> {
    const result = super.open(options);

    this.loadData()
    return result;
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
            this._filterCustomers(null);
          })

        )
        .subscribe()
    );
  }

  protected _readCustomerAddresses() {
    this._subscriptions.push(
      this._addressesService.getCustomerAddresses(this.options.customerId)
        .pipe(
          tap(e => {
            this.customerAddresses = e;
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
    const nuovoAddressSelezionato: AddressModel = nuovoCustomerSelezionato.addresses.find(x => x.isMainAddress == true);
    if (nuovoAddressSelezionato != undefined) {
      this.options.customerAddressId = nuovoAddressSelezionato.id;
    }
  }

  editCustomer() {
    const request: CustomerModel = Object.assign(new CustomerModel(), JSON.parse(JSON.stringify(this.options.customer)));
    this._subscriptions.push(
      this._customerService.getCustomer(this.options.customerId)
        .pipe(
          map(e => {
            return Object.assign(new CustomerModel(), e);
          }),
          switchMap(e => this.customerModal.open(e)),
          filter(e => e),
          map(() => this.customerModal.options),
          switchMap(e => this._customerService.updateCustomer(e, this.options.customerId)),
          map(() => this.customerModal.options),
          tap(e => this._messageBox.success(`Cliente ${e.customerDescription} aggiornato`)),
          tap(() => { this.loadData(); })
        )
        .subscribe()
    );
  }
  editAddress() {
    const request: AddressModel = Object.assign(new AddressModel(), JSON.parse(JSON.stringify(this.options.customerAddress)));
    this._subscriptions.push(
      this._addressesService.getAddress(this.options.customerAddressId)
        .pipe(
          map(e => {
            return Object.assign(new AddressModel(), e);
          }),
          switchMap(e => this.addressModal.open(e)),
          filter(e => e),
          map(() => this.addressModal.options),
          switchMap(e => this._addressesService.updateAddress(e, this.options.customerAddressId)),
          map(() => this.addressModal.options),
          tap(e => this._messageBox.success(`Indirizzo aggiornato con successo`)),
          tap(() => {
            this.customerSelezionato.addresses
          }),
          tap(() => {
            this.loadData();
          })
        )
        .subscribe()
    );
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
            this.loadData();
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
    const a = this.options;
    this._subscriptions.push(
      this.customerModal.open(request)
        .pipe(
          filter(e => e),
          switchMap(() => this._customerService.createCustomer(request)),
          tap(e => {
            this.options.customerId = e.id;
            this.console.log(e.id);
            this._messageBox.success(`Cliente ${request.name} creato`);
          }),
          tap(() => this._readJobCustomers(true)),
          tap(() => this._readCustomerAddresses())
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

  viewLastNote() {
    this.notesModal.id = this.options.id;
    this._subscriptions.push(
      this._notesService.getLastJobNote(this.options.id)
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

  viewJobActivities(jobId: number) {     
    this._subscriptions.push(
            this._jobsService.getJobActivities(jobId)
            .pipe(        
                switchMap(e => this.jobActivitiesModal.open(e))
            )
    .subscribe()
    );
  }

  isVisibleResultNote(): boolean {
    return true;
    // this.options.status == this.jobStatusEnum.Completed ||
    //   this.options.status == this.jobStatusEnum.Billing ||
    //   this.options.status == this.jobStatusEnum.Billed ||
    //   this.options.status == this.jobStatusEnum.Paid ||
    //   this.options.status == this.jobStatusEnum.Warranty;
  }

  handleFilter(value: string) {
    this._filterCustomers(value);
  }

  public loadData() {
    this._readJobCustomers();
    this._readOperators();
    this._readJobSources();
    this._readJobProductTypes();
    this._readCustomerAddresses();
    
    if (this.options.id > 0) {
      this._readJobActivities(this.options.id);
    }
  }

  protected _readJobActivities(id: number) {
    this._subscriptions.push(
      this._jobsService.getJobActivities(id)
        .pipe(
          tap(e => {
            this.jobActivities = e;
          })
        )
        .subscribe()
    );
  }

  private _filterCustomers(value: string) {
    // this.customersFiltered = this.customers;
    if (value == null || value.length < 3) {
      if (this.options.customerId != null) {
        this.customersFiltered = this.customers.filter((s) => s.id == this.options.customerId)
      }
      else {
        this.customersFiltered = [];
      }
    }
    else {
      value = value.toLowerCase();
      //TODO Ottimizzare filtro
      this.customersFiltered = this.customers.filter((s) => s.customerDescription.toLowerCase().indexOf(value) !== -1);
    }

  }
}
