import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { JobDetailModel } from '../shared/models/job-detail.model';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { markAsDirty } from '../services/common/functions';
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

@Component({
  selector: 'app-job-modal',
  templateUrl: './job-modal.component.html',
  styleUrls: ['./job-modal.component.scss']
})
export class JobModalComponent extends ModalComponent<JobDetailModel> {

  @ViewChild('form') form: NgForm;
  readonly role = Role;

  operators: Array<JobOperatorModel> = [];
  customers: Array<CustomerModel> = [];
  sources: Array<JobSourceModel> = [];
  productTypes: Array<ProductTypeModel> = [];
  states: Array<SimpleLookupModel> = [];

  constructor(private readonly _messageBox: MessageBoxService, private readonly _jobsService: JobsService) {
    super();
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

  protected _readJobCustomers() {
    this._subscriptions.push(
      this._jobsService.getJobCustomers()
        .pipe(
            tap(e => {
              this.customers = e;
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

  test() {
    console.log(this.options);
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

  setStates() {
    this.states = [];
    for(var n in JobStatusEnum) {
        if (typeof JobStatusEnum[n] === 'number') {
          this.states.push({id: <any>JobStatusEnum[n], name: n});
        }
    }
  }

  public loadData() {
    this._readOperators();
    this._readJobCustomers();
    this._readJobSources();
    this._readJobProductTypes();
    this.setStates();
  }
}
