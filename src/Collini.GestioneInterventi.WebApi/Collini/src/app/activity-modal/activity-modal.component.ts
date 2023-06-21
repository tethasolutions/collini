import { Component, ViewChild } from '@angular/core';
import { ActivityModel } from '../shared/models/activity.model';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { MessageBoxService } from '../services/common/message-box.service';
import { markAsDirty } from '../services/common/functions';
import { JobOperatorModel } from '../shared/models/job-operator.model';
import { JobsService } from '../services/jobs.service';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { ActivityStatusEnum } from '../shared/enums/activity-status.enum';
import { SimpleLookupModel } from '../shared/models/simple-lookup.model';
import { JobModel } from '../shared/models/job.model';
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';

@Component({
  selector: 'app-activity-modal',
  templateUrl: './activity-modal.component.html',
  styleUrls: ['./activity-modal.component.scss']
})
export class ActivityModalComponent extends ModalComponent<ActivityModel> {

    @ViewChild('form') form: NgForm;
    @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
    readonly role = Role;

    operators: Array<JobOperatorModel> = [];
    jobs: Array<JobModel> = [];
    states: Array<SimpleLookupModel> = [];
    activityNotes: Array<NoteModel> = [];

    constructor(
        private readonly _messageBox: MessageBoxService,
        private readonly _jobsService: JobsService,
        private readonly _notesService: NotesService
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

    protected _readjobs() {
        this._subscriptions.push(
          this._jobsService.getAllJobs()
            .pipe(
                tap(e => {
                  this.jobs = e;
                })
            )
            .subscribe()
        );
    }

    setStates() {
        this.states = [];
        for(var n in ActivityStatusEnum) {
            if (typeof ActivityStatusEnum[n] === 'number') {
              this.states.push({id: <any>ActivityStatusEnum[n], name: n});
            }
        }
    }

    viewNotes() {
        this._subscriptions.push(
          this._notesService.getActivityNotes(this.options.id)
            .pipe(
                map(e => {
                  this.activityNotes = e;
                }),
                switchMap(e => this.notesModal.open(e))
            )
          .subscribe()
        );
      }

    public loadData() {
        this._readOperators();
        this._readjobs();
        this.setStates();
    }
}
