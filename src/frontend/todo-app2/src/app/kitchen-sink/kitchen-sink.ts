import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzNotificationService } from 'ng-zorro-antd/notification';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzStepsModule } from 'ng-zorro-antd/steps';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { NzRateModule } from 'ng-zorro-antd/rate';

@Component({
  selector: 'app-kitchen-sink',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    // Adicione todos os módulos importados aqui
    NzButtonModule, NzIconModule, NzGridModule, NzDividerModule, NzCardModule,
    NzAlertModule, NzModalModule, NzDrawerModule,
    NzInputModule, NzSelectModule, NzDatePickerModule, NzCheckboxModule, NzRadioModule,
    NzSwitchModule, NzFormModule, NzTableModule, NzTagModule, NzBadgeModule,
    NzAvatarModule, NzSpinModule, NzSkeletonModule, NzStepsModule, NzTabsModule,
    NzTimePickerModule, NzRateModule ,NzInputNumberModule
  ],
  templateUrl: './kitchen-sink.html',
  styleUrls: ['./kitchen-sink.scss']
})
export class KitchenSinkComponent implements OnInit {
  // Propriedades para os componentes
  isLoading = false;
  isDrawerVisible = false;
  myForm: FormGroup;

  // Novos FormGroups
  clientForm!: FormGroup;
  orderForm!: FormGroup;
  settingsForm!: FormGroup;
  uploadForm!: FormGroup;

  // Dados para a tabela
  listOfData = [
    { key: '1', name: 'John Brown', age: 32, address: 'New York No. 1 Lake Park' },
    { key: '2', name: 'Jim Green', age: 42, address: 'London No. 1 Lake Park' },
    { key: '3', name: 'Joe Black', age: 32, address: 'Sidney No. 1 Lake Park' }
  ];

  // Dados para componentes específicos
  fileList: any[] = [];
  suggestions = ['Angular', 'React', 'Vue', 'TypeScript', 'JavaScript'];
  idiomaOptions = [
    {
      value: 'pt',
      label: 'Português',
      children: [
        { value: 'pt-br', label: 'Brasil' },
        { value: 'pt-pt', label: 'Portugal' }
      ]
    },
    {
      value: 'en',
      label: 'English',
      children: [
        { value: 'en-us', label: 'United States' },
        { value: 'en-gb', label: 'United Kingdom' }
      ]
    }
  ];

  constructor(
    private message: NzMessageService,
    private notification: NzNotificationService,
    private modal: NzModalService,
    private fb: FormBuilder
  ) {
    this.myForm = this.fb.group({
      name: ['', [Validators.required]],
      option: [null, [Validators.required]],
      date: [null]
    });
  }

  ngOnInit(): void {
    this.initializeForms();
  }

  private initializeForms(): void {
    // Formulário de Cliente
    this.clientForm = this.fb.group({
      nomeCompleto: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      telefone: [''],
      estadoCivil: [''],
      dataNascimento: [''],
      aceitaTermos: [false]
    });

    // Formulário de Pedido
    this.orderForm = this.fb.group({
      numeroPedido: ['', [Validators.required]],
      categoria: ['normal'],
      valor: [0],
      produtos: [[]],
      observacoes: ['']
    });

    // Formulário de Configurações
    this.settingsForm = this.fb.group({
      tema: ['light'],
      notificacoes: [true],
      volume: [50],
      idioma: [[]],
      horarioBackup: ['']
    });

    // Formulário de Upload
    this.uploadForm = this.fb.group({
      avaliacao: [0],
      cor: ['#1890ff'],
      tags: ['']
    });
  }

  // Métodos para os componentes de feedback
  showMessage(): void {
    this.message.success('Esta é uma mensagem de sucesso!');
  }

  showNotification(): void {
    this.notification.info(
      'Título da Notificação',
      'Este é o conteúdo da notificação. Ela permanecerá na tela até ser fechada.'
    );
  }

  showModal(): void {
    this.modal.confirm({
      nzTitle: '<i>Você tem certeza que quer fazer isso?</i>',
      nzContent: '<b>Ao confirmar, esta ação não poderá ser desfeita.</b>',
      nzOnOk: () => console.log('OK')
    });
  }

  // Métodos para o Drawer
  openDrawer(): void {
    this.isDrawerVisible = true;
  }

  closeDrawer(): void {
    this.isDrawerVisible = false;
  }

  // Método para o formulário
  submitForm(): void {
    if (this.myForm.valid) {
      console.log('Formulário enviado:', this.myForm.value);
      this.message.success('Formulário válido e enviado com sucesso!');
    } else {
      Object.values(this.myForm.controls).forEach(control => {
        if (control.invalid) {
          control.markAsDirty();
          control.updateValueAndValidity({ onlySelf: true });
        }
      });
      this.message.error('Por favor, corrija os erros no formulário.');
    }
  }

  resetClientForm(): void {
    this.clientForm.reset();
  }

  // Configuração de formatação para o nz-input-number
  formatCurrency(value: number): string {
    return `R$ ${value}`;
  }

  parseCurrency(value: string): number {
    return parseFloat(value.replace('$ ', ''));
  }
}