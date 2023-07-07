import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { AddressesService } from '../services/addresses.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { Router, NavigationEnd } from '@angular/router';
import { ActivityStatusEnum } from '../shared/enums/activity-status.enum';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { ActivityCalendarModel } from '../shared/models/activity-calendar.model';
import { ActivityModalComponent } from '../activity-modal/activity-modal.component';
import { ActivitiesService } from '../services/activities.service';
import { ActivityModel } from '../shared/models/activity.model';

@Component({
  selector: 'app-activities',
  templateUrl: './activities.component.html',
  styleUrls: ['./activities.component.scss']
})
export class ActivitiesComponent extends BaseComponent implements OnInit {

  @ViewChild('activityModal', { static: true }) activityModal: ActivityModalComponent;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;

  activityNotes: Array<NoteModel> = [];
  
  dataActivities: GridDataResult;
  stateGridActivities: State = {
      skip: 0,
      take: 10,
      filter: {
          filters: [],
          logic: 'and'
      },
      group: [],
      sort: []
  };

  constructor(
      private readonly _activitiesService: ActivitiesService,
      private readonly _notesService: NotesService,
      private readonly _messageBox: MessageBoxService,
      private readonly _router: Router
  ) {
      super();
  }

  ngOnInit() {
      console.log(this._router.url);
      this._readActivities();
  }

  dataStateChange(state: State) {
      this.stateGridActivities = state;
      this._readActivities();
  }

  protected _readActivities() {
    this._subscriptions.push(
      this._activitiesService.readActivities(this.stateGridActivities)
        .pipe(
            tap(e => {
              console.log(e);
              this.dataActivities = e;
            })
        )
        .subscribe()
    );
  }

  createActivity() {
    const request = new ActivityModel();
    this.activityModal.loadData();
    this._subscriptions.push(
      this.activityModal.open(request)
          .pipe(
              filter(e => e),
              switchMap(() => this._activitiesService.createActivity(request)),
              tap(e => {
                this._messageBox.success(`Intervento creato`);
              }),
              tap(() => this._readActivities())
          )
          .subscribe()
    );
  }

  editActivity(activity: ActivityModel) {
    this.activityModal.loadData();
    this._subscriptions.push(
      this._activitiesService.getActivity(activity.id)
        .pipe(
            map(e => {
              return e;
            }),
            switchMap(e => this.activityModal.open(e)),
            filter(e => e),
            map(() => this.activityModal.options),
            switchMap(e => this._activitiesService.updateActivity(e, e.id)),
            map(() => this.activityModal.options),
            tap(e => this._messageBox.success(`Intervento aggiornato`)),
            tap(() => this._readActivities())
        )
      .subscribe()
    );
  }

  viewNotes(activity: ActivityModel) {
    this.notesModal.id = activity.id;
    this.notesModal.loadData();
    this.notesModal.open(null);
    /* this._subscriptions.push(
      this._notesService.getActivityNotes(activity.id)
        .pipe(
            map(e => {
              this.activityNotes = e;
            }),
            switchMap(e => this.notesModal.open(e))
        )
      .subscribe()
    ); */
  }
}
