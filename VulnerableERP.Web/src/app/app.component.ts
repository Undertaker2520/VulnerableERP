import { Component } from '@angular/core';
import { RouterOutlet, RouterModule, Router, NavigationEnd } from '@angular/router';
import { CommonModule } from '@angular/common';
import { filter } from 'rxjs/operators';
import { AuthService } from './core/services/auth.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterModule, CommonModule],
  template: `
    <div class="app-layout">
      <aside class="sidebar" *ngIf="showSidebar">
        <div class="brand">
          <span class="material-icons">business</span>
          <h2>VulnerableERP</h2>
        </div>
        <nav class="nav-menu">
          <a [routerLink]="['/dashboard']" routerLinkActive="active" class="nav-item">
            <span class="material-icons">dashboard</span> Dashboard
          </a>
          <a [routerLink]="['/invoices']" routerLinkActive="active" class="nav-item">
            <span class="material-icons">receipt</span> Fatture
          </a>
          <a [routerLink]="['/tickets']" routerLinkActive="active" class="nav-item">
            <span class="material-icons">support_agent</span> Ticket
          </a>
          <a [routerLink]="['/expense-reports']" routerLinkActive="active" class="nav-item">
            <span class="material-icons">request_quote</span> Note Spese
          </a>
          <a [routerLink]="['/tools']" *ngIf="authService.isAdmin" routerLinkActive="active" class="nav-item">
            <span class="material-icons">build</span> Tools
          </a>
          <a [routerLink]="['/config']" *ngIf="authService.isAdmin" routerLinkActive="active" class="nav-item">
            <span class="material-icons">settings</span> Configurazione
          </a>
        </nav>
      </aside>
      <main class="main-content">
        <header class="top-header" *ngIf="showSidebar">
          <div class="user-info">
            <span class="material-icons">account_circle</span>
            <span>{{authService.username}}</span>
            <button class="erp-button danger" style="padding: 4px 8px; margin-left: 15px;" (click)="logout()">Logout</button>
          </div>
        </header>
        <div class="content-area">
          <router-outlet></router-outlet>
        </div>
      </main>
    </div>
  `,
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'VulnerableERP';
  showSidebar = true;

  constructor(private router: Router, public authService: AuthService) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe((event: any) => {
      this.showSidebar = !(event.urlAfterRedirects.includes('/login') || event.urlAfterRedirects.includes('/register'));
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
