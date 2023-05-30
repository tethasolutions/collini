import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { JobsService } from '../services/jobs.service';
import { AddressesService } from '../services/addresses.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { JobModalComponent } from '../job-modal/job-modal.component';
import { JobModel } from '../shared/models/job.model';
import { AddressModel } from '../shared/models/address.model';
import { AddressModalComponent } from '../address-modal/address-modal.component';
import { Router, NavigationEnd } from '@angular/router';
import { JobStatusEnum } from '../shared/enums/job-status.enum';

@Component({
  selector: 'app-jobs-active',
  templateUrl: './jobs-active.component.html',
  styleUrls: ['./jobs-active.component.scss']
})
export class JobsActiveComponent extends BaseComponent implements OnInit {

  @ViewChild('jobModal', { static: true }) jobModal: JobModalComponent;

  jobType: string;
  
  dataJobs: GridDataResult;
  stateGridJobs: State = {
      skip: 0,
      take: 10,
      filter: {
          filters: [],
          logic: 'and'
      },
      group: [],
      sort: []
  };

  getGiobStatusString(index: number): string {
    if (index >= 0) { return JobStatusEnum[index]; }
    else { return ''; }
  }

  constructor(
      private readonly _jobsService: JobsService,
      private readonly _messageBox: MessageBoxService,
      private readonly _router: Router
  ) {
      super();
  }

  ngOnInit() {
      console.log(this._router.url);
      if (this._router.url === '/jobs/acceptance') { this.jobType = 'acceptance'; }
      if (this._router.url === '/jobs/active') { this.jobType = 'active'; }
      if (this._router.url === '/jobs/billed') { this.jobType = 'billed'; }
      this._readJobs();
  }

  dataStateChange(state: State) {
      this.stateGridJobs = state;
      this._readJobs();
  }

  protected _readJobs() {
    console.log(this.jobType);
    if (this.jobType == undefined) { return; }
    this._subscriptions.push(
      this._jobsService.readJobs(this.stateGridJobs, this.jobType)
        .pipe(
            tap(e => {
              console.log(e);
              this.dataJobs = e;
            })
        )
        .subscribe()
    );
  }

  createJob() {
    const request = new JobModel();
    this._subscriptions.push(
      this.jobModal.open(request)
          .pipe(
              filter(e => e),
              // switchMap(() => {
                // this._customerService.createCustomer(request);
              // }),
              tap(e => {
                /* if (this.anagraficaType == 'customers') { 
                  this._messageBox.success(`Cliente ${request.name} creato`);
                }
                if (this.anagraficaType == 'providers') { 
                  this._messageBox.success(`Fornitore ${request.name} creato`);
                } */
              }),
              tap(() => this._readJobs())
          )
          .subscribe()
    );
  }
}
