import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

@Component({
  selector: 'app-tickets',
  standalone: true,
  imports: [CommonModule, FormsModule],
  template: `
    <div class="erp-header">Supporto e Ticket</div>

    <div class="erp-card">
      <h3>Ricerca Ticket</h3>
      <div style="display: flex; gap: 10px;">
        <input type="text" [(ngModel)]="searchQuery" placeholder="Cerca per titolo..." class="erp-input" style="max-width: 300px;">
        <button class="erp-button" (click)="searchTickets()">Cerca</button>
      </div>
    </div>

    <div class="erp-card">
      <h3>Crea Nuovo Ticket</h3>
      <form (ngSubmit)="createTicket()" style="max-width: 500px;">
        <div class="form-group">
          <label>Titolo</label>
          <input type="text" [(ngModel)]="newTicket.title" name="title" class="erp-input" required>
        </div>
        <div class="form-group">
          <label>Descrizione</label>
          <textarea [(ngModel)]="newTicket.description" name="description" class="erp-input" rows="3" required></textarea>
        </div>
        <button type="submit" class="erp-button">Invia Ticket</button>
      </form>
    </div>

    <div class="erp-card">
      <h3>I tuoi Ticket</h3>
      <button class="erp-button" (click)="loadTickets()" style="margin-bottom: 15px;">Aggiorna Lista</button>
      <table class="erp-table">
        <thead>
          <tr>
            <th>ID</th>
            <th>Titolo</th>
            <th>Descrizione</th>
            <th>Creatore</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let t of tickets">
            <td>{{t.id}}</td>
            <td>{{t.title}}</td>
            <!-- VULNERABILITY: Bypassiamo il sanitizer nativo di Angular per esporre la falla XSS del backend -->
            <td [innerHTML]="bypassSanitization(t.description)"></td>
            <td>{{t.username}}</td>
          </tr>
        </tbody>
      </table>
    </div>
  `,
  styles: [`
    .form-group { margin-bottom: 15px; }
    .erp-input { width: 100%; padding: 10px; border: 1px solid #cbd5e1; border-radius: 4px; box-sizing: border-box; margin-top: 5px; }
  `]
})
export class TicketsComponent implements OnInit {
  tickets: any[] = [];
  searchQuery = '';
  newTicket = { title: '', description: '' };

  constructor(private http: HttpClient, private sanitizer: DomSanitizer, private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    this.loadTickets();
  }

  bypassSanitization(content: string): SafeHtml {
    // Angular protegge di default. Dato che l'obiettivo è dimostrare la falla del backend,
    // bypassiamo la protezione frontend qui.
    return this.sanitizer.bypassSecurityTrustHtml(content);
  }

  loadTickets() {
    this.http.get<any[]>('https://localhost:44313/api/Tickets').subscribe(data => {
      this.tickets = data;
      this.cdr.detectChanges();
    });
  }

  searchTickets() {
    this.http.get<any[]>(`https://localhost:44313/api/Tickets/search?q=${this.searchQuery}`).subscribe(data => this.tickets = data);
  }

  createTicket() {
    this.http.post('https://localhost:44313/api/Tickets', this.newTicket).subscribe({
      next: () => {
        alert('Ticket creato!');
        this.newTicket = { title: '', description: '' };
        this.loadTickets();
      },
      error: (err) => alert('Errore: ' + err.message)
    });
  }
}
