import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { CustomerService } from '../services/customer.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { CustomerModalComponent } from '../customer-modal/customer-modal.component';
import { CustomerModel } from '../shared/models/customer.model';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss']
})
export class CustomersComponent extends BaseComponent implements OnInit {

  @ViewChild('customerModal', { static: true }) customerModal: CustomerModalComponent;

  dataCustomers: GridDataResult;
  stateGridCustomers: State = {
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
      private readonly _customerService: CustomerService,
      private readonly _messageBox: MessageBoxService
  ) {
      super();
  }

  ngOnInit() {
      this._readCustomers();
  }

  dataStateChange(state: State) {
      this.stateGridCustomers = state;
      this._readCustomers();
  }

  protected _readCustomers() {
    this._subscriptions.push(
      this._customerService.readCustomers(this.stateGridCustomers)
        .pipe(
            tap(e => {
              this.dataCustomers = e;
              console.log(this.dataCustomers);
            })
        )
        .subscribe()
    );
  }

  createCustomer() {
    const request = new CustomerModel();

    this._subscriptions.push(
        this.customerModal.open(request)
            .pipe(
                filter(e => e),
                switchMap(() => this._customerService.createCustomer(request)),
                tap(e => this._messageBox.success(`Cliente ${request.name} creato`)),
                tap(() => this._readCustomers())
            )
            .subscribe()
    );
  }

  editCustomer(customer: CustomerModel) {
    console.log(customer);

    this._subscriptions.push(
      this._customerService.getCustomer(customer.customerSupplierId)
        .pipe(
            map(e => {
              return Object.assign(new CustomerModel(), e);
            }),
            switchMap(e => this.customerModal.open(e)),
            filter(e => e),
            map(() => this.customerModal.options),
            switchMap(e => this._customerService.updateCustomer(e, customer.customerSupplierId)),
            map(() => this.customerModal.options),
            tap(e => this._messageBox.success(`Cliente ${e.name} aggiornato`)),
            tap(() => this._readCustomers())
        )
      .subscribe()
    );
  }
}
