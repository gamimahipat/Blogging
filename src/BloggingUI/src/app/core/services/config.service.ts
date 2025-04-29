import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ConfigService {
  private config: any | null = null;

  constructor(private http: HttpClient) { }

  async loadConfig(): Promise<void> {
    this.config = await firstValueFrom(this.http.get('/webconfig.json'));
  }

  get(key: string): any {
    return this.config ? this.config[key] : null;
  }
 
}
