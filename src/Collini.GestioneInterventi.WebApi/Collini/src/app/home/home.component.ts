import { Component } from '@angular/core';
import { BaseComponent } from '../shared/base.component';
import { JobCountersModel } from '../shared/models/job-counters.model';
import { Router } from '@angular/router';
import { JobsService } from '../services/jobs.service';
import { tap } from 'rxjs/operators';

@Component({
    selector: 'collini-home',
    templateUrl: 'home.component.html',
    styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent {

    jobCounters = new JobCountersModel();

    constructor(private router: Router, private readonly _jobsService: JobsService) {
        super();
    }
  
    protected getJobCounters() {
      this._subscriptions.push(
        this._jobsService.getJobCounters()
          .pipe(
              tap(e => {
                console.log(e);
                this.jobCounters = e;
              })
          )
          .subscribe()
      );
    }
  
    ngOnInit() {
      this.getJobCounters();
    }
    
}
