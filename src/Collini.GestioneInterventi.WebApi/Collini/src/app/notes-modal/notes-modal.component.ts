import { Component, ViewChild, Input } from '@angular/core';
import { ModalComponent } from '../shared/modal.component';
import { AddressModel } from '../shared/models/address.model';
import { NgForm } from '@angular/forms';
import { markAsDirty } from '../services/common/functions';
import { MessageBoxService } from '../services/common/message-box.service';
import { Role } from '../services/security/models';
import { NoteModel } from '../shared/models/note.model';

@Component({
  selector: 'app-notes-modal',
  templateUrl: './notes-modal.component.html',
  styleUrls: ['./notes-modal.component.scss']
})

export class NotesModalComponent extends ModalComponent<any> {

    @Input() note: Array<NoteModel> = [];

    constructor() {
        super();
    }

    aggiungiNota() {
        
    }
    
    modificaNota(nota: NoteModel) {
        console.log(nota);
    }

    protected _canClose() {
        return true;
    }
}
