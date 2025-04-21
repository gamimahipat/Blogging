import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { AdminDashboardComponent } from './layouts/admin-components/admin-dashboard/admin-dashboard.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login', data: { hideNavbar: true } },
  { path: 'login', component: LoginComponent, data: { hideNavbar: true } },
  { path: 'admin-dashboard', component: AdminDashboardComponent },

]

