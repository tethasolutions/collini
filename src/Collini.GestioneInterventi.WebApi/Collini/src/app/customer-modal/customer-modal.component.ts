import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { CustomerModel } from '../shared/models/customer.model';
import { AddressModel } from '../shared/models/address.model';
import { NgForm } from '@angular/forms';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { Role } from '../services/security/models';
import { AddressModalComponent } from '../address-modal/address-modal.component';
import { filter, map, switchMap, tap } from 'rxjs/operators';

@Component({
  selector: 'app-customer-modal',
  templateUrl: './customer-modal.component.html',
  styleUrls: ['./customer-modal.component.scss']
})

export class CustomerModalComponent extends ModalComponent<CustomerModel> {
  
    @ViewChild('form') form: NgForm;
    @ViewChild('addressModal', { static: true }) addressModal: AddressModalComponent;

    readonly role = Role;

    constructor(
        private readonly _messageBox: MessageBoxService
    ) {
        super();
    }

    protected _canClose() {
        markAsDirty(this.form);

        if (this.form.invalid) {
            this._messageBox.error('Compilare correttamente tutti i campi');
        }

        return this.form.valid;
    }

    mainAddressChanged(address: AddressModel) {
        if (address === undefined) { return; }
        this.options.addresses.forEach((item: AddressModel) => {
            item.isMainAddress = item.addressId === address.addressId;
        });
    }

    addNewAddress(address: AddressModel) {
        this.options.addresses.push(address);
        if (address.isMainAddress) {
            this.options.addresses.forEach((item: AddressModel) => {
                item.isMainAddress = item.addressId === address.addressId;
            });
        }
    }

    createAddress() {
        const request = new AddressModel();
        request.customerId = this.options.customerSupplierId;
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
}
