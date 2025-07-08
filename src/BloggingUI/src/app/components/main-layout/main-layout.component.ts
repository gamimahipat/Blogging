import { Component } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { SidebarComponent } from '../sidebar/sidebar.component';

import { RouterModule } from '@angular/router';

@Component({
    selector: 'app-main-layout',
    imports: [RouterModule, SidebarComponent, NavbarComponent],
    templateUrl: './main-layout.component.html',
    styleUrl: './main-layout.component.css'
})
export class MainLayoutComponent {
  isSidebarOpen = true;


  toggleSidebar() {
    this.isSidebarOpen = !this.isSidebarOpen;
  }

  logout() {
    localStorage.clear();
    location.href = '/login';
  }
}
