import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { CustomerModel } from '../shared/models/customer.model';
import { NgForm } from '@angular/forms';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { Role } from '../services/security/models';

@Component({
  selector: 'app-customer-modal',
  templateUrl: './customer-modal.component.html',
  styleUrls: ['./customer-modal.component.scss']
})

export class CustomerModalComponent extends ModalComponent<CustomerModel> {
  
    @ViewChild('form')
    form: NgForm;

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
}
