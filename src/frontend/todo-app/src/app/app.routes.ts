import { Routes } from '@angular/router';
import { MainLayout } from './layouts/main-layout/main-layout';
import { authGuard } from './core/guards/auth-guard';

export const routes: Routes = [
 {
    path: 'login',
    loadComponent: () => import('./pages/auth/login/login').then(m => m.Login)
  },
  {
    path: 'register',
    loadComponent: () => import('./pages/auth/register/register').then(m => m.Register)
  },

  // Rota do layout principal, agora protegida pelo guard
  {
    path: '',
    component: MainLayout,
    canActivate: [authGuard], // <-- 2. APLIQUE O GUARD AQUI
    children: [
      {
        path: 'todo-all',
        loadComponent: () => import('./pages/todo-all/todo-all').then(m => m.TodoAll)
      },
      {
        path: 'welcome',
        loadComponent: () => import('./pages/welcome/welcome').then(m => m.Welcome)
      },
      { path: '', redirectTo: 'welcome', pathMatch: 'full' }
    ]
  },

  { path: '**', redirectTo: 'login' }
];
