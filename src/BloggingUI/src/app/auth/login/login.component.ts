import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { UsersService } from '../../core/services/users.service';
import { NotificationService } from '../../core/services/notification.service';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, MatInputModule,
    MatFormFieldModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  loginForm!: FormGroup;
  isLoading = false;
  apiURL = 'https://localhost:44341/';

  //constructor(private fb: FormBuilder, private userService: UsersService, private router: Router, private notification: NotificationService) { }
  hide = true;

  constructor(private fb: FormBuilder, private userService: UsersService, private router: Router, private notification: NotificationService) {
  }
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

      this.userService.login(body).subscribe((response) => {
        this.isLoading = false;
        if (response.success) {
          //alert(response.message);
          this.notification.showNotification('success', response.message);
          this.router.navigate(['/admin-dashboard']);
        } else {
          this.notification.showNotification('danger', response.message);
        }
      },
        (errorMsg) => {
          this.isLoading = false;
        });
    } else {
      console.log('Invalid form');
    }
  }
}
