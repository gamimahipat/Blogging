import { ConfigService } from './config.service';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotificationService } from './notification.service';

@Injectable({
  providedIn: 'root',
})
export class BaseService {

  private configService = inject(ConfigService);
  apiUrl = this.configService.get('apiUrl');
  constructor(private http: HttpClient, private notification: NotificationService) { }

  request(method: 'GET' | 'POST' | 'PUT' | 'DELETE', endpoint: string,
    options: {
      body?: any;
      params?: HttpParams | { [param: string]: string | number | boolean };
      isFormData?: boolean;
      customHeaders?: HttpHeaders | { [header: string]: string };
    } = {}
  ): Observable<any> {
    let headers = new HttpHeaders;

    if (!options.isFormData) {
      headers = headers.set('Content-Type', 'application/json').set('Accept', 'application/json');
    }

    const url = `${this.apiUrl}/${endpoint}`;

    return this.http.request(method, url, {
      body: options.body,
      params: options.params,
      headers,
      observe: 'body',
    }).pipe(catchError(this.handleError)
    );
  }

  handleError = (error: HttpErrorResponse) => {
    let errorMessage = 'An unknown error occurred.';

    // 🔹 Handle client-side or network errors
    if (error.error instanceof ErrorEvent) {
      errorMessage = `Client error: ${error.error.message}`;
    }

    // 🔹 Handle server-side errors
    else {
      switch (error.status) {
        case 0:
          errorMessage = 'Unable to connect to the server.';
          break;
        case 400:
          errorMessage = 'Bad request.';
          break;
        case 401:
        case 403:
          errorMessage = 'You are not authorized. Redirecting to login...';
          //this.router.navigate(['/login']);
          break;
        case 404:
          errorMessage = 'Resource not found.';
          break;
        case 500:
          errorMessage = 'Internal server error.';
          break;
        default:
          errorMessage = `Error ${error.status}: ${error.message}`;
          break;
      }
    }

    // 🔔 Show notification (if you have a notification service)
    this.notification.showNotification('danger', errorMessage);

    // 🛑 Always return an observable error
    return throwError(() => errorMessage);
  };
}

