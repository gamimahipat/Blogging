import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm!: FormGroup;
  isLoading = false;
  apiURL = 'https://localhost:44341/';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.createForm();
    //if (this.authGuard.canActivate())
    //  this.router.navigate(['/digidarshanregister']);
  }

  createForm() {
    this.loginForm = this.fb.group({
      username: ['', [Validators.required]],
      password: ['', [Validators.required]]
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.isLoading = true;

      const body = {
        userName: this.loginForm.get('username')?.value,
        password: this.loginForm.get('password')?.value,
      }

    //  this.digiDarshanService.login(body).subscribe((response) => {
    //    this.isLoading = false;
    //    if (response.isSuccess) {
    //      alert(response.message);
    //      this.authService.login();
    //      this.router.navigate(['/digidarshanregister']);
    //    } else {
    //      alert(response.message);
    //    }
    //  },
    //    (error) => {
    //      this.isLoading = false;
    //      alert('The server is refusing the connection.');
    //    }
    //  )
    //} else {
    //  console.log('Invalid form');
    }
  }
}
