// src/app/core/guards/auth.guard.ts

import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

/**
 * Guarda de rota que verifica se o usuário está autenticado.
 * Se o usuário estiver autenticado, permite o acesso à rota.
 * Se não estiver, redireciona para a página de login.
 */
export const authGuard: CanActivateFn = (route, state) => {
  
  // Injeta o serviço de autenticação e o roteador
  const authService = inject(AuthService);
  const router = inject(Router);

  // Usa o método do serviço para verificar se há um token válido
  if (authService.isAuthenticated()) {
    return true; // ✅ Acesso permitido
  }

  // Se não estiver autenticado, cria uma "UrlTree" para redirecionar
  // para a página de login.
  return router.createUrlTree(['/login']); // 🛑 Acesso bloqueado e redirecionado
};