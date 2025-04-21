import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginData = { userName: '', password: '' };
  loading = false;
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) { }

  login() {
    this.loading = true;
    this.errorMessage = '';

    this.authService.login(this.loginData).subscribe({
      next: (response) => {
        this.loading = false;
        localStorage.setItem('token', response.token);
        this.router.navigate(['/dashboard']); // Redirect after login
      },
      error: (error) => {
        this.loading = false;
        this.errorMessage = error.error.message || 'Login failed!';
      }
    });
  }
}
