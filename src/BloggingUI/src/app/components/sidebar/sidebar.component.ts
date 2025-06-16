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
  @Input() closed: boolean = false;
  constructor() { }

  ngOnInit(): void {
    //const roles = this.authService.getRoles(); // e.g., ['Admin']

    //if (roles.includes('Admin')) {
      this.menuItems = [
        { label: 'Dashboard', path: '/admin/dashboard' },
        { label: 'Manage Posts', path: '/admin/posts' },
        { label: 'Users', path: '/admin/users' }
      ];
    //} else if (roles.includes('User')) {
    //  this.menuItems = [
    //    { label: 'Dashboard', path: '/user/dashboard' },
    //    { label: 'My Posts', path: '/user/my-posts' },
    //    { label: 'Create Post', path: '/user/create' }
    //  ];
    //}
  }

  role = localStorage.getItem('role'); // For example

  isAdmin(): boolean {
    return this.role === 'Admin';
  }

  isUser(): boolean {
    return this.role === 'User';
  }
}
