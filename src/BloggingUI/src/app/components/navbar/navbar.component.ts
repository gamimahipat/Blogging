import { Component, EventEmitter, Output, signal } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isSidebarOpen = true;
  constructor() { }


  //toggleSidebar() {
  //  this.isSidebarOpen = !this.isSidebarOpen;
  //}

  //logout() {
  //  // Clear token & redirect
  //  localStorage.clear();
  //  location.href = '/login';
  //}

  @Output() menuToggle = new EventEmitter<void>();
  @Output() logoutEvent = new EventEmitter<void>();

  toggleMenu() {
    this.menuToggle.emit();
  }

  logout() {
    this.logoutEvent.emit();
  }
}
