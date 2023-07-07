import { Component, OnInit, ViewChild } from '@angular/core';
import { GridDataResult } from '@progress/kendo-angular-grid';
import { OrdersService } from '../services/orders.service';
import { AddressesService } from '../services/addresses.service';
import { MessageBoxService } from '../services/common/message-box.service';
import { BaseComponent } from '../shared/base.component';
import { State } from '@progress/kendo-data-query';
import { filter, map, switchMap, tap } from 'rxjs/operators';
import { Router, NavigationEnd } from '@angular/router';
import { OrderStatusEnum } from '../shared/enums/order-status.enum';
import { NotesModalComponent } from '../notes-modal/notes-modal.component';
import { NotesService } from '../services/notes.service';
import { NoteModel } from '../shared/models/note.model';
import { OrderModalComponent } from '../order-modal/order-modal.component';
import { OrderDetailModel } from '../shared/models/order-detail.model';

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent extends BaseComponent implements OnInit {

  @ViewChild('orderModal', { static: true }) orderModal: OrderModalComponent;
  @ViewChild('notesModal', { static: true }) notesModal: NotesModalComponent;

  orderNotes: Array<NoteModel> = [];
  
  dataOrders: GridDataResult;
  stateGridOrders: State = {
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
      private readonly _ordersService: OrdersService,
      private readonly _notesService: NotesService,
      private readonly _messageBox: MessageBoxService,
      private readonly _router: Router
  ) {
      super();
  }

  ngOnInit() {
      console.log(this._router.url);
      this._readOrders();
  }

  dataStateChange(state: State) {
      this.stateGridOrders = state;
      this._readOrders();
  }

  protected _readOrders() {
    this._subscriptions.push(
      this._ordersService.readOrders(this.stateGridOrders)
        .pipe(
            tap(e => {
              console.log(e);
              this.dataOrders = e;
            })
        )
        .subscribe()
    );
  }

  createOrder() {
    const request = new OrderDetailModel();
    this.orderModal.loadData();
    this._subscriptions.push(
      this.orderModal.open(request)
          .pipe(
              filter(e => e),
              switchMap(() => this._ordersService.createOrder(request)),
              tap(e => {
                this._messageBox.success(`Ordine creato`);
              }),
              tap(() => this._readOrders())
          )
          .subscribe()
    );
  }

  editOrder(order: OrderDetailModel) {
    this.orderModal.loadData();
    this._subscriptions.push(
      this._ordersService.getOrderDetail(order.id)
        .pipe(
            map(e => {
              return e;
            }),
            switchMap(e => this.orderModal.open(e)),
            filter(e => e),
            map(() => this.orderModal.options),
            switchMap(e => this._ordersService.updateOrder(e, e.id)),
            map(() => this.orderModal.options),
            tap(e => this._messageBox.success(`Ordine aggiornato`)),
            tap(() => this._readOrders())
        )
      .subscribe()
    );
  }

  viewNotes(order: OrderDetailModel) {
    this.notesModal.id = order.id;
    this.notesModal.loadData();
    this.notesModal.open(null);
    /* this._subscriptions.push(
      this._notesService.getOrderNotes(order.id)
        .pipe(
            map(e => {
              this.orderNotes = e;
            }),
            switchMap(e => this.notesModal.open(e))
        )
      .subscribe()
    ); */
  }
}
