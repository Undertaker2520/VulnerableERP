import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-expense-reports',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="erp-header">Gestione Note Spese</div>
    <div class="erp-card">
      <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
        <h3>Le Mie Note Spese</h3>
        <button class="erp-button">Nuova Nota Spesa</button>
      </div>
      <table class="erp-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Data</th>
            <th>Importo</th>
            <th>Descrizione</th>
            <th>Stato</th>
            <th>Azioni</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let report of reports">
            <td>{{report.id}}</td>
            <td>{{report.date | date}}</td>
            <td>€{{report.amount}}</td>
            <td>{{report.description}}</td>
            <td>
              <span class="status-badge" [ngClass]="report.status.toLowerCase()">{{report.status}}</span>
            </td>
            <td>
                <button *ngIf="isAdmin && report.status === 'Pending'"
                  class="erp-button warning" 
                  style="padding: 4px 8px; font-size: 0.8rem;" 
                  (click)="forceApprove(report.id)">
                  Approva
                </button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
  styleUrls: ['./expense-reports.component.scss']
})
export class ExpenseReportsComponent implements OnInit {
  reports: any[] = [];
  isAdmin = false;

  constructor(private http: HttpClient, private authService: AuthService) {
    this.isAdmin = this.authService.isAdmin;
  }

  ngOnInit() {
    this.loadExpenses();
  }

  loadExpenses() {
    this.http.get<any[]>('https://localhost:44313/api/Expenses').subscribe({
      next: (data) => this.reports = data,
      error: (err) => console.error('Errore nel caricamento note spese', err)
    });
  }

  forceApprove(id: number) {
    this.http.patch('https://localhost:44313/api/Expenses/status', {
      expenseId: id,
      newStatus: 'Approved'
    }).subscribe({
      next: () => {
        alert('Nota spesa approvata con successo.');
        this.loadExpenses();
      },
      error: (err) => alert('Errore: ' + err.message)
    });
  }
}
