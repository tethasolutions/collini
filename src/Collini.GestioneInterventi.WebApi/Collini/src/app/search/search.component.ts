import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { AddressesService } from '../services/addresses.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { Router, NavigationEnd } from '@angular/router';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { SearchService } from '../services/search.service';
import { JobDetailModel } from '../shared/models/job-detail.model';
import { JobSearchModel } from '../shared/models/job-search.model';
import { NoteModalComponent } from '../note-modal/note-modal.component';
import { JobModel } from '../shared/models/job.model';
import { JobsService } from '../services/jobs.service';
import { JobModalComponent } from '../job-modal/job-modal.component';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent extends BaseComponent implements OnInit {

  @ViewChild('jobModal', { static: true }) jobModal: JobModalComponent;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;
  @ViewChild('noteModal', { static: true }) noteModal: NoteModalComponent;

  searchNotes: Array<NoteModel> = [];
  
  dataSearch: GridDataResult;
  stateGridSearch: State = {
      skip: 0,
      take: 15,
      filter: {
          filters: [],
          logic: 'and'
      },
      group: [],
      sort: []
  };

  constructor(
      private readonly _searchService: SearchService,
      private readonly _jobsService: JobsService,
      private readonly _notesService: NotesService,
      private readonly _messageBox: MessageBoxService,
      private readonly _router: Router
  ) {
      super();
  }

  ngOnInit() {
      console.log(this._router.url);
      this._readJobs();
  }

  dataStateChange(state: State) {
      this.stateGridSearch = state;
      this._readJobs();
  }

  protected _readJobs() {
    this._subscriptions.push(
      this._searchService.readJobs(this.stateGridSearch)
        .pipe(
            tap(e => {
              console.log(e);
              this.dataSearch = e;
            })
        )
        .subscribe()
    );
  }

  editJob(job: JobModel) {

    this._subscriptions.push(
        this._jobsService.getJobDetail(job.id)
            .pipe(
                map(e => {
                    return e;
                }),
                switchMap(e => this.jobModal.open(e)),
                filter(e => e),
                map(() => this.jobModal.options),
                switchMap(e => this._jobsService.updateJob(e, e.id)),
                map(() => this.jobModal.options),
                tap(e => this._messageBox.success(`Job '${e.code}' aggiornato`)),
                tap(() => this._readJobs())
            )
            .subscribe()
    );
}

  viewNotes(job: JobSearchModel) {
    this.notesModal.id = job.id;
    this.notesModal.loadData();
    this.notesModal.open(null);
  }
  
  viewLastNote(job: JobSearchModel) {     
    this.notesModal.id = job.id;
    this._subscriptions.push(
            this._notesService.getLastJobNote(job.id)
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
}
