import { Component, ViewChild } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { JobModel } from '../shared/models/job.model';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';

@Component({
  selector: 'app-job-modal',
  templateUrl: './job-modal.component.html',
  styleUrls: ['./job-modal.component.scss']
})
export class JobModalComponent extends ModalComponent<JobModel> {

  @ViewChild('form') form: NgForm;
  readonly role = Role;

  constructor(private readonly _messageBox: MessageBoxService) {
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
