import { APP_INITIALIZER, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
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
import { DropDownButtonModule } from '@progress/kendo-angular-buttons';
import { DateInputsModule } from '@progress/kendo-angular-dateinputs';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { DialogsModule } from '@progress/kendo-angular-dialog';
import { NotificationModule } from '@progress/kendo-angular-notification';
import { IntlModule } from '@progress/kendo-angular-intl';
import { FormsModule } from '@angular/forms';
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
        JobsComponent
    ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        HttpClientModule,
        FormsModule,
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
        PDFModule
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
        Clipboard,
        AuthGuard,
        CustomerService,
        AddressesService,
        JobsService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
