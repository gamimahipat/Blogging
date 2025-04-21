import { Component, signal } from '@angular/core';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  isMenuOpen = signal(false);  
  constructor() { }


  toggleMenu() {
    this.isMenuOpen.set(!this.isMenuOpen());
  }
}
