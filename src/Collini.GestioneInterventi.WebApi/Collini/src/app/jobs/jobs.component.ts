import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { JobsService } from '../services/jobs.service';
import { AddressesService } from '../services/addresses.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { CustomerModalComponent } from '../customer-modal/customer-modal.component';
import { CustomerModel } from '../shared/models/customer.model';
import { AddressModel } from '../shared/models/address.model';
import { AddressModalComponent } from '../address-modal/address-modal.component';
import { Router, NavigationEnd } from '@angular/router';
import { JobStatusEnum } from '../shared/enums/job-status.enum';

@Component({
  selector: 'app-jobs',
  templateUrl: './jobs.component.html',
  styleUrls: ['./jobs.component.scss']
})

export class JobsComponent extends BaseComponent implements OnInit {

  constructor(private router: Router) {
      super();
  }

  ngOnInit() {
    if (this.router.routerState.snapshot.url === '/jobs') {
      this.router.navigate(['/jobs/acceptance']);
    }
  }
}
