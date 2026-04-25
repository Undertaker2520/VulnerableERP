import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ExpenseReportsComponent } from './features/expense-reports/expense-reports.component';
import { LoginComponent } from './features/auth/login.component';
import { RegisterComponent } from './features/auth/register.component';
import { TicketsComponent } from './features/tickets/tickets.component';
import { InvoicesComponent } from './features/invoices/invoices.component';
import { ToolsComponent } from './features/tools/tools.component';
import { SystemComponent } from './features/system/system.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent },
  { path: 'expense-reports', component: ExpenseReportsComponent },
  { path: 'tickets', component: TicketsComponent },
  { path: 'invoices', component: InvoicesComponent },
  { path: 'tools', component: ToolsComponent },
  { path: 'config', component: SystemComponent },
  { path: '**', redirectTo: '/dashboard' }
];
