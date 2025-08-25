import { Component, inject } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { AuthService } from '../../core/services/auth.service';
// ... outras importações
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzTooltipModule } from 'ng-zorro-antd/tooltip';
@Component({
  selector: 'app-main-layout',
  standalone: true,
  imports: [[RouterLink, RouterOutlet, NzIconModule, NzLayoutModule, NzMenuModule, NzButtonModule, NzTooltipModule]],
  templateUrl: './main-layout.html', // Caminho corrigido
  styleUrls: ['./main-layout.scss']   // Caminho corrigido
})
export class MainLayout { // Nome da classe corrigido
  isCollapsed = false;
  authService = inject(AuthService);
    fazerLogout(): void {
    this.authService.logout();
    // O método logout() no serviço já cuida de remover o token e redirecionar
  }
}

