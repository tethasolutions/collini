import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './services/guards/auth.guard';
import { UsersComponent } from './security/users.component';
import { LogoutComponent } from './security/logout.component';
import { LoginComponent } from './security/login.component';
import { HomeComponent } from './home/home.component';
import { CustomersComponent } from './customers/customers.component';
import { JobsComponent } from './jobs/jobs.component';
import { JobsActiveComponent } from './jobs-active/jobs-active.component';
import { CalendarComponent } from './calendar/calendar.component';
import { QuotationsComponent } from './quotations/quotations.component';
import { OrdersComponent } from './orders/orders.component';
import { ActivitiesComponent } from './activities/activities.component';

const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent, canActivate: [AuthGuard.asInjectableGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'logout', component: LogoutComponent },
    { path: 'users', component: UsersComponent, canActivate: [AuthGuard.asInjectableGuard] },
    { path: 'customers', component: CustomersComponent, canActivate: [AuthGuard.asInjectableGuard] },
    { path: 'providers', component: CustomersComponent, canActivate: [AuthGuard.asInjectableGuard] },
    { path: 'jobs', component: JobsComponent, canActivate: [AuthGuard.asInjectableGuard],
        children: [
            {
                path: '',
                component: JobsActiveComponent
            },
            {
                path: 'acceptance',
                component: JobsActiveComponent
            },
            {
                path: 'active',
                component: JobsActiveComponent
            },
            {
                path: 'billed',
                component: JobsActiveComponent
            },
            { 
                path: 'quotations', 
                component: QuotationsComponent 
            },
            { 
                path: 'orders', 
                component: OrdersComponent 
            },
            { 
                path: 'activities', 
                component: ActivitiesComponent
            }
        ]
    },
    { path: 'orders', component: OrdersComponent },
    { path: 'calendar', component: CalendarComponent, canActivate: [AuthGuard.asInjectableGuard] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes, { useHash: true, onSameUrlNavigation: 'reload' })],
    exports: [RouterModule]
})
export class AppRoutingModule { }
