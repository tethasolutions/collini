import { APP_INITIALIZER, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent} from './app.component';
import { LoaderComponent } from './shared/loader.component';
import { ValidationMessageComponent } from './shared/validation-message.component';
import { UsersComponent } from './security/users.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './security/login.component';
import { LogoutComponent } from './security/logout.component';
import { UserModalComponent } from './security/user-modal.component';
import { registerLocaleData } from '@angular/common';
import localeIt from '@angular/common/locales/it';
import localeExtraIt from '@angular/common/locales/extra/it';
import '@progress/kendo-angular-intl/locales/it/all';
import '@angular/common/locales/global/it';
import { ExcelModule, GridModule, PDFModule } from '@progress/kendo-angular-grid';
import { PDFExportModule } from '@progress/kendo-angular-pdf-export';
import { InputsModule, NumericTextBoxModule, SwitchModule } from '@progress/kendo-angular-inputs';
import { ButtonsModule, DropDownButtonModule } from '@progress/kendo-angular-buttons';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { SchedulerModule } from '@progress/kendo-angular-scheduler';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { IntlModule } from '@progress/kendo-angular-intl';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { TooltipsModule } from "@progress/kendo-angular-tooltip";
import { StorageService } from './services/common/storage.service';
import { UserService } from './services/security/user.service';
import { SecurityService, refreshUserData } from './services/security/security.service';
import { HeadersInterceptor } from './services/interceptors/headers.interceptor';
import { ResponseInterceptor } from './services/interceptors/response.interceptor';
import { MessageBoxService } from './services/common/message-box.service';
import { Router } from '@angular/router';
import { LoaderService } from './services/common/loader.service';
import { LoaderInterceptor } from './services/interceptors/loader.interceptor';
import { AuthGuard } from './services/guards/auth.guard';
import { MenuComponent } from './menu/menu.component';
import { BooleanPipe } from './pipes/boolean.pipe';
import { CustomersComponent } from './customers/customers.component';
import { CustomerService } from './services/customer.service';
import { CustomerModalComponent } from './customer-modal/customer-modal.component';
import { AddressesService } from './services/addresses.service';
import { AddressModalComponent } from './address-modal/address-modal.component';
import { AddressesModalComponent } from './addresses-modal/addresses-modal.component';
import { JobsService } from './services/jobs.service';
import { JobsComponent } from './jobs/jobs.component';
import { JobsActiveComponent } from './jobs-active/jobs-active.component';
import { JobModalComponent } from './job-modal/job-modal.component';
import { CalendarComponent } from './calendar/calendar.component';
import { ActivityModalComponent } from './activity-modal/activity-modal.component';
import { ActivitiesService } from './services/activities.service';
import { NotesService } from './services/notes.service';
import { NotesModalComponent } from './notes-modal/notes-modal.component';
import { NoteModalComponent } from './note-modal/note-modal.component';
import { NoteAttachmentsModalComponent } from './note-attachments-modal/note-attachments-modal.component';
import { NoteAttachmentModalComponent } from './note-attachment-modal/note-attachment-modal.component';
import { JobStatusEnum } from './shared/enums/job-status.enum';
import { JobStatusPipe } from './pipes/job-status.pipe';
import { QuotationsComponent } from './quotations/quotations.component';
import { QuotationsService } from './services/quotations.service';
import { QuotationStatusPipe } from './pipes/quotation-status.pipe';
import { OrderStatusEnum } from './shared/enums/order-status.enum';
import { OrdersComponent } from './orders/orders.component';
import { OrderStatusPipe } from './pipes/order-status.pipe';
import { OrdersService } from './services/orders.service';
import { QuotationModalComponent} from './quotation-modal/quotation-modal.component';
import { OrderModalComponent } from './order-modal/order-modal.component';
import { RolePipe } from './pipes/role.pipe';
import { ActivitiesComponent } from './activities/activities.component';
import { ActivityStatusPipe } from './pipes/activity-status.pipe';
import { CopyActivityModalComponent } from './copy-activity-modal/copy-activity-modal.component';
import { UploadsModule } from "@progress/kendo-angular-upload";


import { UploadInterceptor } from './services/interceptors/upload.iterceptor';
import { EditorModule } from '@progress/kendo-angular-editor';



registerLocaleData(localeIt, 'it', localeExtraIt);

@NgModule({
    declarations: [
        BooleanPipe,
        AppComponent,
        
        LoaderComponent,
        ValidationMessageComponent,
        HomeComponent,
        UsersComponent,
        LoginComponent,
        LogoutComponent,
        UserModalComponent,
        MenuComponent,
        CustomersComponent,
        CustomerModalComponent,
        AddressModalComponent,
        AddressesModalComponent,
        JobsComponent,
        JobsActiveComponent,
        JobModalComponent,
        QuotationsComponent,
        QuotationModalComponent,
        OrdersComponent,
        OrderModalComponent,
        CalendarComponent,
        ActivityModalComponent,
        ActivitiesComponent,
        CopyActivityModalComponent,
        NotesModalComponent,
        NoteModalComponent,
        NoteAttachmentsModalComponent,
        NoteAttachmentModalComponent,
        JobStatusPipe,
        QuotationStatusPipe,
        OrderStatusPipe,
        ActivityStatusPipe,
        RolePipe
    ],
    imports: [ 
        ButtonsModule,
        BrowserModule,        
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
         UploadsModule,
        IntlModule,
        AppRoutingModule,
        NotificationModule,
        GridModule,
        DialogsModule,
        DropDownsModule,
        NumericTextBoxModule,
        DateInputsModule,
        TooltipsModule,
        DropDownButtonModule,
        SwitchModule,
        PDFExportModule,
        ExcelModule,
        InputsModule,
        PDFModule,
        SchedulerModule,
        EditorModule
    ],
    providers: [
        {
            provide: LOCALE_ID, useValue: 'it'
        },
        StorageService,
        UserService,
        SecurityService,
        { provide: APP_INITIALIZER, useFactory: refreshUserData, multi: true, deps: [SecurityService, UserService] },
        { provide: HTTP_INTERCEPTORS, useClass: HeadersInterceptor, multi: true, deps: [UserService] },
        MessageBoxService,
        { provide: HTTP_INTERCEPTORS, useClass: ResponseInterceptor, multi: true, deps: [Router, UserService, MessageBoxService] },
        LoaderService,
        { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true, deps: [LoaderService] },

        { provide: HTTP_INTERCEPTORS, useClass: UploadInterceptor, multi: true },

        Clipboard,
        AuthGuard,
        CustomerService,
        AddressesService,
        JobsService,
        QuotationsService,
        OrdersService,
        ActivitiesService,
        NotesService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
