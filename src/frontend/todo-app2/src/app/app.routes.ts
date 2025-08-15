import { Routes } from '@angular/router';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: '/welcome' },
  { path: 'welcome', loadChildren: () => import('./pages/welcome/welcome.routes').then(m => m.WELCOME_ROUTES) },
  { path: 'todo-all', loadChildren: () => import('./pages/todo-all/todo-all.routes').then(m => m.TODO_ALL_ROUTES) },
    // ADICIONE A ROTA PARA O KITCHEN SINK AQUI
  { path: 'kitchen-sink', loadChildren: () => import('./kitchen-sink/kitchen-sink.routes').then(m => m.KITCHEN_SINK_ROUTES) },

];
