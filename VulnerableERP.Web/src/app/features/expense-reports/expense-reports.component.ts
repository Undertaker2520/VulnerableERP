import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-expense-reports',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="erp-header">Gestione Note Spese</div>
    <div class="erp-card" *ngIf="showForm">
      <h3>Crea Nuova Nota Spesa</h3>
      <form (ngSubmit)="createExpense()" style="max-width: 500px;">
        <div class="form-group" style="margin-bottom: 15px;">
          <label style="display: block; margin-bottom: 5px;">Importo (€)</label>
          <input type="number" [(ngModel)]="newExpense.amount" name="amount" class="erp-input" style="width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 4px;" required>
        </div>
        <div class="form-group" style="margin-bottom: 15px;">
          <label style="display: block; margin-bottom: 5px;">Descrizione</label>
          <input type="text" [(ngModel)]="newExpense.description" name="description" class="erp-input" style="width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 4px;" required>
        </div>
        <div style="display: flex; gap: 10px;">
          <button type="submit" class="erp-button">Salva</button>
          <button type="button" class="erp-button secondary" (click)="showForm = false" style="background: #64748b;">Annulla</button>
        </div>
      </form>
    </div>

    <div class="erp-card">
      <div style="display: flex; justify-content: space-between; align-items: center; margin-bottom: 20px;">
        <h3>Le Mie Note Spese</h3>
        <button class="erp-button" (click)="showForm = !showForm">Nuova Nota Spesa</button>
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
  showForm = false;
  newExpense = { amount: 0, description: '' };

  constructor(private http: HttpClient, private authService: AuthService, private cdr: ChangeDetectorRef) {
    this.isAdmin = this.authService.isAdmin;
  }

  ngOnInit() {
    this.loadExpenses();
  }

  loadExpenses() {
    this.http.get<any[]>('https://localhost:44313/api/Expenses').subscribe({
      next: (data) => {
        this.reports = data;
        this.cdr.detectChanges();
      },
      error: (err) => console.error('Errore nel caricamento note spese', err)
    });
  }

  createExpense() {
    this.http.post('https://localhost:44313/api/Expenses', this.newExpense).subscribe({
      next: () => {
        alert('Nota spesa creata!');
        this.showForm = false;
        this.newExpense = { amount: 0, description: '' };
        this.loadExpenses();
      },
      error: (err) => alert('Errore: ' + err.message)
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
