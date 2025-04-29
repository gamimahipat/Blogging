import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  controllerName: any
  constructor(private http: HttpClient, private baseService: BaseService) {
    this.controllerName = "Users"
}

  login(body: any) {
    const url = `${this.controllerName}/AuthenticateUser`;
    return this.baseService.request("POST", url, { body });
  }
  
 

}
