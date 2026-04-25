import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  template: `
    <div class="login-container">
      <div class="erp-card login-card">
        <h3>Registrazione</h3>
        <div class="error-msg" *ngIf="errorMessage">{{ errorMessage }}</div>
        
        <form (ngSubmit)="register()">
          <div class="form-group">
            <label>Username</label>
            <input type="text" [(ngModel)]="userData.username" name="username" class="erp-input" required>
          </div>
          <div class="form-group">
            <label>Email</label>
            <input type="email" [(ngModel)]="userData.email" name="email" class="erp-input" required>
          </div>
          <div class="form-group">
            <label>Password</label>
            <input type="password" [(ngModel)]="userData.password" name="password" class="erp-input" required>
          </div>

          <button type="submit" class="erp-button" style="width: 100%;">Registrati</button>
        </form>
        
        <div style="margin-top: 15px; text-align: center;">
          <a routerLink="/login">Torna al login</a>
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
    .login-card { width: 100%; max-width: 400px; padding: 30px; }
    .form-group { margin-bottom: 15px; }
    .erp-input { width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 4px; box-sizing: border-box; margin-top: 5px; }
    .error-msg { color: var(--danger-color); margin-bottom: 15px; font-size: 0.9rem; }
  `]
})
export class RegisterComponent {
  userData = { username: '', email: '', password: '' };
  errorMessage = '';

  constructor(private authService: AuthService, private router: Router) {}

  register() {
    this.authService.register(this.userData).subscribe({
      next: (res) => {
        alert('Registrazione completata con successo!');
        this.router.navigate(['/login']);
      },
      error: (err) => this.errorMessage = err.error || 'Registrazione fallita'
    });
  }
}
