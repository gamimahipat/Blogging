import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { AdminDashboardComponent } from './layouts/admin-components/admin-dashboard/admin-dashboard.component';
import { PublicDashboardComponent } from './layouts/public-components/public-dashboard/public-dashboard.component';
import { MainLayoutComponent } from './components/main-layout/main-layout.component';
import { adminRoutes } from './layouts/admin-components/admin-routing';
import { publicRoutes } from './layouts/public-components/public-routing';

//export const routes: Routes = [
//  { path: '', pathMatch: 'full', redirectTo: 'login', data: { hideNavbar: true } },
//  { path: 'login', component: LoginComponent, data: { hideNavbar: true } },
//  { path: 'admin-dashboard', component: AdminDashboardComponent },
//  { path: 'public-dashboard', component: PublicDashboardComponent },

//]

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'login', data: { hideNavbar: true } },
  {
    path: 'login',
    loadComponent: () =>
      import('./auth/login/login.component').then(m => m.LoginComponent),
    data: { hideNavbar: true }
  },

  {
    path: '',
    component: MainLayoutComponent, 
    children: adminRoutes,
  },
  {
    path: '',
    component: MainLayoutComponent,
    children: publicRoutes
  },
  {
    path: '**',
    redirectTo: 'login',
    data: { hideNavbar: true }
  }
  
];


