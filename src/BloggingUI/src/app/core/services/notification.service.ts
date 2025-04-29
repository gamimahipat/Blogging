import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor(private toastr: ToastrService) { }

  showNotification(type: 'success' | 'danger' | 'info' | 'warning', message: string): void {
    switch (type) {
      case 'success':
        this.toastr.success(message, 'Success');
        break;
      case 'danger':
        this.toastr.error(message, 'Error');
        break;
      case 'info':
        this.toastr.info(message, 'Info');
        break;
      case 'warning':
        this.toastr.warning(message, 'Warning');
        break;
    }
  }
}
