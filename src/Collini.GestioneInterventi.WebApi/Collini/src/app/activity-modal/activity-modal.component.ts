import { Component, ViewChild } from '@angular/core';
import { ActivityModel } from '../shared/models/activity.model';
import { ModalComponent } from '../shared/modal.component';
import { NgForm } from '@angular/forms';
import { Role } from '../services/security/models';
import { MessageBoxService } from '../services/common/message-box.service';
import { listEnum, markAsDirty } from '../services/common/functions';
import { JobOperatorModel } from '../shared/models/job-operator.model';
import { JobsService } from '../services/jobs.service';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { ActivityStatusEnum } from '../shared/enums/activity-status.enum';
import { SimpleLookupModel } from '../shared/models/simple-lookup.model';
import { JobModel } from '../shared/models/job.model';
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { Observable } from 'rxjs';
import { CopyActivityModalComponent } from '../copy-activity-modal/copy-activity-modal.component';

@Component({
  selector: 'app-activity-modal',
  templateUrl: './activity-modal.component.html',
  styleUrls: ['./activity-modal.component.scss']
})
export class ActivityModalComponent extends ModalComponent<ActivityModel> {

    @ViewChild('form') form: NgForm;
    @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
    @ViewChild('copyActivityModal', { static: true }) copyActivityModal: CopyActivityModalComponent;
    readonly role = Role;

    operators: Array<JobOperatorModel> = [];
    jobs: Array<JobModel> = [];
    jobsFiltered: Array<JobModel> = [];
    states= listEnum<ActivityStatusEnum>(ActivityStatusEnum);
    activityNotes: Array<NoteModel> = [];

    constructor(
        private readonly _messageBox: MessageBoxService,
        private readonly _jobsService: JobsService,
        private readonly _notesService: NotesService
    ) {
        super();
    }

    override open(options: ActivityModel): Observable<boolean> 
    {
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

    protected _readjobs() {
      this._subscriptions.push(
        this._jobsService.getAllJobs()
          .pipe(
              tap(e => {
                this.jobs = e;
              }),
              tap(()=> this._filterJobs(null))
          )
          .subscribe()
      );
  }

  copyActivity() {
    this.copyActivityModal.open(this.options.id)
    .pipe(
      filter (x=>x), 
      tap(()=> this.dismiss())
    )
    .subscribe()
  }

    viewNotes() {
        this.notesModal.id = this.options.id;
        this.notesModal.loadData();
        this.notesModal.open(null);
        /* this._subscriptions.push(
          this._notesService.getActivityNotes(this.options.id)
            .pipe(
                map(e => {
                  this.activityNotes = e;
                }),
                switchMap(e => this.notesModal.open(e))
            )
          .subscribe()
        ); */


        /* this._subscriptions.push(
          this.notesModal.open(null)
              .pipe(
                  filter(e => e),
                  tap(e => {
                      this.customer.addresses.push(request);
                      if (request.isMainAddress) {
                          this.customer.mainAddress = request;
                          this.customer.addresses.forEach((item: AddressModel) => {
                              item.isMainAddress = item.tempId === request.tempId;
                          });
                      }
                      this._messageBox.success(`Indirizzo creato con successo`);
                  })
              )
              .subscribe()
        ); */
      }

      handleFilter(value:string) 
      {
        this._filterJobs(value);
      }


    public loadData() 
    {
      this._readjobs();
      
      this._readOperators();
    }

    private _filterJobs(value:string)
    {    

      if(value == null || value.length < 3)
      {
        if(this.options != null && this.options.jobId != null)
        {
          this.jobsFiltered = this.jobs.filter((s)=> s.id == this.options.jobId)        
        }
        else
        {        
          this.jobsFiltered = [];
        }        
      }
      else
      {        
        value = value.toLowerCase();
        //TODO Ottimizzare filtro
        this.jobsFiltered =this.jobs.filter((s)=> s.fullDescription.toLowerCase().indexOf(value)!== -1);
      }

    }
}
