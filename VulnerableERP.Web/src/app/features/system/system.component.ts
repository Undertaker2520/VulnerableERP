import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-system',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="erp-header">Configurazione di Sistema</div>
    
    <div class="erp-card">
      <h3>Diagnostica di Sistema</h3>
      <p>
        Utilizza questo strumento per esportare le variabili d'ambiente correnti a scopo di debug. 
        I dati esportati contengono informazioni critiche sul setup del server.
      </p>
      <button class="erp-button" (click)="getEnvInfo()">Esporta Variabili d'Ambiente</button>
    </div>

    <div class="erp-card" *ngIf="envData">
      <h3>Dati Esposti:</h3>
      <table class="erp-table">
        <thead>
          <tr>
            <th>Chiave (Key)</th>
            <th>Valore Segreto (Value)</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let item of envData | keyvalue">
            <td style="font-family: monospace;">{{item.key}}</td>
            <td style="font-family: monospace; color: #d32f2f;">{{item.value}}</td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
  styles: [`
  `]
})
export class SystemComponent {
  envData: any = null;

  constructor(private http: HttpClient) {}

  getEnvInfo() {
    this.http.get('https://localhost:44313/api/System/debug/env').subscribe({
      next: (data) => this.envData = data,
      error: (err) => alert('Errore: ' + err.message)
    });
  }
}
