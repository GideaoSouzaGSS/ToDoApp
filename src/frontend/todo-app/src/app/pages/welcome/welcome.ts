import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router'; // Importe para usar o routerLink no botão

// Módulos do NG-ZORRO
import { NzResultModule } from 'ng-zorro-antd/result';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzButtonModule } from 'ng-zorro-antd/button'; // Para o botão de CTA

// ❗️ MÓDULO NOVO ❗️
import { NzStatisticModule } from 'ng-zorro-antd/statistic';

@Component({
  selector: 'app-welcome',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule, // Adicione o RouterModule
    // Módulos NG-ZORRO
    NzResultModule,
    NzCardModule,
    NzGridModule,
    NzListModule,
    NzIconModule,
    NzButtonModule,
    NzStatisticModule // Adicione o novo módulo aqui
  ],
  templateUrl: './welcome.html',
  styleUrls: ['./welcome.scss']
})
export class Welcome {}