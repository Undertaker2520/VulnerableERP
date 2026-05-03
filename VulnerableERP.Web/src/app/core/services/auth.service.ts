import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // Configured to point to the backend's auth endpoint
  private apiUrl = 'https://localhost:44313/api/auth';
  private tokenKey = 'erp_auth_token';

  constructor(private http: HttpClient) { }

  login(credentials: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/login`, credentials).pipe(
      tap((response: any) => {
        if (response && response.token) {
          this.setToken(response.token);
        }
      })
    );
  }

  register(userData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, userData);
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  get isAdmin(): boolean {
    const token = this.getToken();
    if (!token) return false;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload.role === 'Admin' || payload.isAdmin === 'True';
    } catch {
      return false;
    }
  }

  get username(): string {
    const token = this.getToken();
    if (!token) return 'Utente ERP';
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      // .NET ClaimTypes.Name often maps to this long URL or 'unique_name'
      return payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'] || 
             payload.unique_name || 
             payload.name || 
             payload.sub || 
             'Utente ERP';
    } catch {
      return 'Utente ERP';
    }
  }
}
