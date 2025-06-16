import { NgIf } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterModule, NgIf],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  menuItems: { label: string, path: string }[] = [];
  @Input() hideSidebar: boolean = false;
  constructor() { }

  ngOnInit(): void {
      this.menuItems = [
        { label: 'Dashboard', path: '/admin/dashboard' },
        { label: 'Manage Posts', path: '/admin/posts' },
        { label: 'Users', path: '/admin/users' }
      ];
    
  }

  role = localStorage.getItem('role'); // For example

  isAdmin(): boolean {
    return this.role === 'Admin';
  }

  isUser(): boolean {
    return this.role === 'User';
  }
}
