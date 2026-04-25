import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="erp-header">Dashboard Overview</div>
    <div class="stats-grid">
      <div class="erp-card stat-card">
        <h3>Utenti Attivi</h3>
        <div class="value">124</div>
      </div>
      <div class="erp-card stat-card">
        <h3>Fatture da Pagare</h3>
        <div class="value">15</div>
      </div>
      <div class="erp-card stat-card">
        <h3>Note Spese in Attesa</h3>
        <div class="value">8</div>
      </div>
    </div>
  `,
  styles: [`
    .stats-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); gap: 20px; margin-bottom: 20px;}
    .stat-card h3 { margin-top: 0; color: #64748b; font-size: 1rem; }
    .stat-card .value { font-size: 2.5rem; font-weight: 700; color: var(--primary-color); }
  `]
})
export class DashboardComponent implements OnInit {
  currentToken: string | null = '';

  constructor(private authService: AuthService) {}

  ngOnInit() {
    this.currentToken = this.authService.getToken();
  }
}
