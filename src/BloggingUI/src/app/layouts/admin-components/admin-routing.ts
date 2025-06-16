import { RouterModule, Routes } from "@angular/router";
import { AdminDashboardComponent } from "./admin-dashboard/admin-dashboard.component";
import { NgModule } from "@angular/core";

export const adminRoutes: Routes = [
  { path: 'admin-dashboard', component: AdminDashboardComponent },
  /*{ path: 'posts', component: PostsComponent },*/
  /*{ path: 'users', component: UsersComponent },*/
  /*{ path: '', redirectTo: 'dashboard', pathMatch: 'full' },*/
];

