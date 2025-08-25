import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { DragDropModule, CdkDragEnter, CdkDragExit, CdkDragDrop } from '@angular/cdk/drag-drop';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzFormModule } from 'ng-zorro-antd/form';

interface Tarefa {
  id: string;
  parentId: string;
  parent: Tarefa | null;
  titulo: string;
  descricao: string;
  dataCriacao: string;
  concluido: boolean;
  ativo: boolean;
  subTarefas: Tarefa[];
}

@Component({
  selector: 'app-tarefa',
  templateUrl: './todo-all.html',
  standalone: true,
  imports: [CommonModule, NzTableModule, NzButtonModule, NzIconModule, NzFormModule, DragDropModule]
})
export class TodoAll implements OnInit {
  tarefas: Tarefa[] = [];
  expandSet = new Set<string>();
  targetTaskId: string | null = null;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get<Tarefa[]>('http://localhost:5001/todos').subscribe(response => {
      this.tarefas = response;
    });
  }

  onEdit(tarefa: Tarefa): void {
    console.log('Editar tarefa:', tarefa);
  }

  onExpandChange(id: string, checked: boolean): void {
    if (checked) {
      this.expandSet.add(id);
    } else {
      this.expandSet.delete(id);
    }
  }

  onAddTask(): void {
    const novaTarefa: Tarefa = {
      id: Date.now().toString(),
      parentId: '',
      parent: null,
      titulo: 'Nova Tarefa',
      descricao: 'Descrição da nova tarefa',
      dataCriacao: new Date().toISOString(),
      concluido: false,
      ativo: true,
      subTarefas: []
    };
    this.tarefas = [...this.tarefas, novaTarefa];
  }

  onDrop(event: CdkDragDrop<Tarefa[]>): void {
    console.log('Drop event:', event);
    const draggedTask = event.item.data;
    
    // Se o item foi solto na mesma lista, não faz nada
    if (event.previousContainer === event.container) {
      return;
    }
    
    const targetTask = event.container.data[0]; // Primeira (e única) tarefa na lista alvo
    
    if (targetTask && draggedTask.id !== targetTask.id) {
      // Adiciona a tarefa arrastada como subtarefa da tarefa alvo
      targetTask.subTarefas.push(draggedTask);
      
      // Remove a tarefa arrastada da lista principal
      this.tarefas = this.tarefas.filter(tarefa => tarefa.id !== draggedTask.id);
      
      console.log('Tarefas atualizadas:', this.tarefas);
    }
    
    // Limpa o rastreamento da tarefa alvo
    this.targetTaskId = null;
  }

  onDragEnter(event: CdkDragEnter): void {
    console.log('Drag Enter Event:', event);
    const targetTask = event.container.data[0]; // Primeira tarefa da lista alvo
    const draggedTask = event.item.data;
    
    // Só marca como alvo se não for a mesma tarefa
    if (targetTask && draggedTask.id !== targetTask.id) {
      this.targetTaskId = targetTask.id;
      console.log('Target Task ID:', this.targetTaskId);
    }
  }

  onDragExit(event: CdkDragExit): void {
    console.log('Drag Exit Event:', event);
    // Limpa o alvo quando sai da área
    this.targetTaskId = null;
  }
}