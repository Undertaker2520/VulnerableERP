import { Component, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-tools',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="erp-header">Integrazioni e Webhooks</div>

    <div class="erp-card">
      <h3>Diagnostica Servizi Esterni</h3>
      <p>Utilizza questo strumento per verificare la comunicazione tra il server ERP e endpoint esterni configurati.</p>
      
      <div style="display: flex; gap: 10px;">
        <input type="text" [(ngModel)]="ssrfUrl" placeholder="Inserisci URL del servizio..." class="erp-input" style="flex: 1;">
        <button class="erp-button" (click)="fireSSRF()">Verifica Connessione</button>
      </div>
      <div *ngIf="ssrfResult" class="result-box">
        <h4>Risposta del Server Esterno:</h4>
        <pre>{{ssrfResult}}</pre>
      </div>
    </div>

    <div class="erp-card">
      <h3>Gestione Impostazioni Avanzate</h3>
      <p>
        Carica i metadati di configurazione in formato testuale per ripristinare le preferenze dell'area di lavoro.
      </p>
      <div class="form-group">
        <textarea [(ngModel)]="jsonPayload" class="erp-input" rows="8"></textarea>
      </div>
      <button class="erp-button" (click)="fireDeserialization()">Avvia Importazione</button>
      <div *ngIf="deserializationResult" class="result-box" style="margin-top:10px;">
        <h4>Risultato Importazione:</h4>
        <pre>{{deserializationResult}}</pre>
      </div>
    </div>
  `,
  styles: [`
    .form-group { margin-bottom: 15px; }
    .erp-input { width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 4px; box-sizing: border-box; margin-top: 5px; font-family: monospace;}
    .result-box { background: #1e1e1e; color: #a5d6ff; padding: 15px; border-radius: 4px; overflow-x: auto; margin-top:15px;}
  `]
})
export class ToolsComponent {
  ssrfUrl = ''; 
  ssrfResult = '';

  jsonPayload = `{
  "Theme": "Dark",
  "Language": "IT",
  "NotificationsEnabled": true
}`;
  deserializationResult = '';

  constructor(private http: HttpClient, private cdr: ChangeDetectorRef) {}

  fireSSRF() {
    this.http.post('https://localhost:44313/api/External/proxy', { url: this.ssrfUrl }).subscribe({
      next: (res: any) => {
        this.ssrfResult = res.content || JSON.stringify(res);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.ssrfResult = 'Errore di connessione';
        this.cdr.detectChanges();
      }
    });
  }

  fireDeserialization() {
    this.http.post('https://localhost:44313/api/Import/settings', { jsonConfig: this.jsonPayload }).subscribe({
      next: (res: any) => {
        this.deserializationResult = res.message || JSON.stringify(res);
        this.cdr.detectChanges();
      },
      error: (err) => {
        this.deserializationResult = 'Errore durante l\'importazione';
        this.cdr.detectChanges();
      }
    });
  }
}
