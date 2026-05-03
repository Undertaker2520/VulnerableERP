import { Routes } from '@angular/router';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { ExpenseReportsComponent } from './features/expense-reports/expense-reports.component';
import { LoginComponent } from './features/auth/login.component';
import { RegisterComponent } from './features/auth/register.component';
import { TicketsComponent } from './features/tickets/tickets.component';
import { InvoicesComponent } from './features/invoices/invoices.component';
import { ToolsComponent } from './features/tools/tools.component';
import { SystemComponent } from './features/system/system.component';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [authGuard] },
  { path: 'expense-reports', component: ExpenseReportsComponent, canActivate: [authGuard] },
  {path: 'tickets', component: TicketsComponent, canActivate: [authGuard] },
  { path: 'invoices', component: InvoicesComponent, canActivate: [authGuard] },
  { path: 'tools', component: ToolsComponent, canActivate: [adminGuard] },
  { path: 'config', component: SystemComponent, canActivate: [adminGuard] },
  { path: '**', redirectTo: '/dashboard' }
];
