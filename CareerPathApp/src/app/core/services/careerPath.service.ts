import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class _CareerPathService {
  constructor(private http: HttpClient) { }

  testConnection() {
    return this.http.get(`${environment.apiUrl}User/TestConnection`);
  }
}