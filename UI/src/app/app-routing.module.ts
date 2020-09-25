import { AuthService } from './services/auth.service';
import { CustomerComplaintComponent } from './customer-complaint/customer-complaint.component';
import { AdminComplaintComponent } from './admin-complaint/admin-complaint.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'admin',
    component: AdminComplaintComponent,
    canActivate: [AuthService]
  },
  {
    path: 'customer',
    component: CustomerComplaintComponent,
    canActivate: [AuthService]
  },
  {
    path: '**',
    component: LoginComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
