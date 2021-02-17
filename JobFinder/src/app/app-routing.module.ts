import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { JobsListComponent } from './jobs/jobs-list/jobs-list.component';
import { Register } from './authorization/registration/Models/Register';
import { RegistrationComponent } from './authorization/registration/registration.component';
import { AuthGuard } from './authorization/auth-guard.service';
import { LoginComponent } from './authorization/login/login.component';
import { UserDataComponent } from './authorization/user/user-data/user-data.component';
import { FirstRegistrationComponent } from './authorization/first-registration/first-registration.component';
import { AdminRequestListComponent } from './admin/admin-request-list/admin-request-list.component';

const routes: Routes = [
  {
    path: 'jobs/list',
    component: JobsListComponent,
  },
  {
    path: 'auth',
    component: LoginComponent
  },
  {
    path: 'register',
    component: FirstRegistrationComponent
  },
  {
    path: '',
    component: LoginComponent,
    canActivate: [AuthGuard],

  },
  {
    path: 'user/data',
    component: UserDataComponent,
    canActivate: [AuthGuard],
  },
  {
    path: 'admin/requests',
    component: AdminRequestListComponent,
    canActivate: [AuthGuard],
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
