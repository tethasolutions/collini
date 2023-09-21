import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { MessageBoxService } from '../services/common/message-box.service';
import { SecurityService } from '../services/security/security.service';
import { IUser, Role, UpdateUserRequest } from '../services/security/models';
import { BaseComponent } from '../shared/base.component';
import { UserModalComponent } from './user-modal.component';

@Component({
    selector: 'collini-users',
    templateUrl: 'users.component.html',
    styleUrls: ['./users.component.scss']
})
export class UsersComponent extends BaseComponent implements OnInit {

    @ViewChild('userModal', { static: true })
    userModal: UserModalComponent;

    data: GridDataResult;
    state: State = {
        skip: 0,
        take: 15,
        group: [],
        sort: []
    };

    constructor(
        private readonly _service: SecurityService,
        private readonly _messageBox: MessageBoxService
    ) {
        super();
    }

    ngOnInit() {
        this._read();
    }

    dataStateChange(state: State) {
        this.state = state;
        this._read();
    }

    create() {
        const request = UpdateUserRequest.empty();

        this._subscriptions.push(
            this.userModal.open(request)
                .pipe(
                    filter(e => e),
                    switchMap(() => this._service.createUser(request)),
                    tap(e => this._messageBox.success(`Utente ${e.userName} creato`)),
                    tap(() => this._read())
                )
                .subscribe()
        );
    }

    edit(user: IUser) {
        this._subscriptions.push(
            this._service.getUser(user.id)
                .pipe(
                    map(e => UpdateUserRequest.fromUser(e)),
                    switchMap(e => this.userModal.open(e)),
                    filter(e => e),
                    map(() => this.userModal.options),
                    switchMap(e => this._service.updateUser(e, user.id)),
                    map(() => this.userModal.options),
                    tap(e => this._messageBox.success(`Utente ${e.userName} aggiornato`)),
                    tap(() => this._read())
                )
                .subscribe()
        );
    }

    protected _read() {
        this._subscriptions.push(
            this._service.readUsers(this.state)
                .pipe(
                    tap(e => this.data = e)
                )
                .subscribe()
        );
    }

}
