import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const adminGuard = () => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isLoggedIn() && authService.isAdmin) {
    return true;
  }

  // Redirect to dashboard if not an admin
  router.navigate(['/dashboard']);
  return false;
};
