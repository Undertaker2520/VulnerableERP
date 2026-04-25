import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <div class="login-container">
      <div class="erp-card login-card">
        <div class="brand">
          <span class="material-icons">business</span>
          <h2>VulnerableERP</h2>
        </div>
        <h3>Accesso</h3>
        <div class="error-msg" *ngIf="errorMessage">{{ errorMessage }}</div>
        
        <form (ngSubmit)="login()">
          <div class="form-group">
            <label>Username</label>
            <input type="text" [(ngModel)]="credentials.username" name="username" class="erp-input" required>
          </div>
          <div class="form-group">
            <label>Password</label>
            <input type="password" [(ngModel)]="credentials.password" name="password" class="erp-input" required>
          </div>
          <button type="submit" class="erp-button" style="width: 100%;">Accedi</button>
        </form>
        
        <div style="margin-top: 15px; text-align: center;">
          <a routerLink="/register">Non hai un account? Registrati</a>
        </div>
      </div>
    </div>
  `,
  styles: [`
    .login-container {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
      background-color: #f1f5f9;
    }
    .login-card {
      width: 100%;
      max-width: 400px;
      padding: 30px;
    }
    .brand {
      display: flex;
      justify-content: center;
      align-items: center;
      gap: 10px;
      margin-bottom: 20px;
      color: #0f172a;
    }
    .form-group {
      margin-bottom: 15px;
    }
    .erp-input {
      width: 100%;
      padding: 10px;
      border: 1px solid #cbd5e1;
      border-radius: 4px;
      box-sizing: border-box;
      margin-top: 5px;
    }
    .error-msg {
      color: var(--danger-color);
      margin-bottom: 15px;
      font-size: 0.9rem;
    }
  `]
})
export class LoginComponent {
  credentials = { username: '', password: '' };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  login() {
    this.authService.login(this.credentials).subscribe({
      next: () => this.router.navigate(['/dashboard']),
      error: (err) => this.errorMessage = err.error?.message || 'Login fallito'
    });
  }
}
