import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-invoices',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="erp-header">Gestione Fatture</div>

    <div class="erp-card">
      <h3>Ricerca Rapida Fattura</h3>
      <div style="display: flex; gap: 10px;">
        <input type="number" [(ngModel)]="searchId" placeholder="ID Fattura" class="erp-input" style="max-width: 150px;">
        <button class="erp-button" (click)="searchInvoice()">Cerca per ID</button>
      </div>
      <div *ngIf="foundInvoice" class="invoice-result">
        <strong>Fattura Trovata!</strong><br>
        ID: {{foundInvoice.id}}<br>
        Importo: €{{foundInvoice.amount}}<br>
        Cliente: {{foundInvoice.clientName}}<br>
        Intestatario: {{foundInvoice.userId}}
      </div>
    </div>

    <div class="erp-card">
      <h3>Applica Codice Sconto</h3>
      <div style="display: flex; gap: 10px; align-items: flex-end;">
        <div class="form-group" style="margin-bottom:0;">
          <label>ID Fattura</label>
          <input type="number" [(ngModel)]="couponPayload.invoiceId" class="erp-input">
        </div>
        <div class="form-group" style="margin-bottom:0;">
          <label>Codice Coupon</label>
          <input type="text" [(ngModel)]="couponPayload.code" class="erp-input">
        </div>
        <button class="erp-button" (click)="applyCoupon()">Applica Sconto</button>
      </div>
      <div class="race-results" *ngIf="couponResult">
        <strong>Risultato:</strong> {{couponResult}}
      </div>
    </div>
  `,
  styles: [`
    .form-group { margin-bottom: 15px; }
    .erp-input { width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 4px; box-sizing: border-box; margin-top: 5px; }
    .invoice-result { margin-top: 15px; padding: 15px; background: white; border: 1px dashed #cbd5e1; border-radius: 4px; }
    .race-results { margin-top: 15px; background: #f8fafc; padding: 10px; border: 1px solid #e2e8f0; border-radius: 4px;}
  `]
})
export class InvoicesComponent {
  searchId: number | null = null;
  foundInvoice: any = null;

  couponPayload = { invoiceId: 1, code: 'SCONTO50' };
  couponResult: string = '';

  constructor(private http: HttpClient) {}

  searchInvoice() {
    if (!this.searchId) return;
    this.http.get(`https://localhost:44313/api/Invoices/${this.searchId}`).subscribe({
      next: (data) => this.foundInvoice = data,
      error: (err) => alert('Fattura non trovata o errore di sistema')
    });
  }

  applyCoupon() {
    this.couponResult = 'Elaborazione in corso...';
    this.http.post('https://localhost:44313/api/Coupon/apply', this.couponPayload, { responseType: 'text' as 'json' })
      .subscribe({
        next: (res: any) => {
          try {
             this.couponResult = JSON.parse(res).message;
          } catch(e) { this.couponResult = res; }
        },
        error: (err) => this.couponResult = 'Errore durante l\'applicazione dello sconto'
      });
  }
}
