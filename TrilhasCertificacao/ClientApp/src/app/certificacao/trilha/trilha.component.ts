import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CertificacaoService } from '../certificacao.service';
import { TrilhaService } from '../trilha.service';
import { Trilha } from '../trilha.type';

@Component({
  selector: 'app-trilha',
  templateUrl: './trilha.component.html',
  styleUrls: ['./trilha.component.css']
})
export class TrilhaComponent implements OnInit {
  certificacoes: any[];
  trilha: Trilha;
  trilhas: Trilha[];

  constructor(private certificacaoService: CertificacaoService,
    private trilhaService: TrilhaService) {
    certificacaoService.getAll().subscribe((res: any[]) => this.certificacoes = res);

    this.new();
    this.load();
  }

  ngOnInit(): void {
  }

  load() {
    this.trilhaService.getAll().subscribe(res => this.trilhas = res);
  }

  new() {
    this.trilha = { certificacoes: [], descricao: '', ano: 0, notificar: false, ativo: false };
  }

  addItem() {
    this.trilha.certificacoes.push({ certificacaoId: '', sequencia: '', trilhaId: '', certificacao: null });
  }

  save() {
    if (this.trilha.id) {
      this.update();
    } else {
      this.add();
    }
  }

  update() {
    this.trilhaService.update(this.trilha.id, this.trilha).subscribe(res => {
      window.alert('sucesso');
      this.new();
      this.load();      
    });
  }

  add() {
    this.trilhaService.add(this.trilha).subscribe(res => {
      window.alert('sucesso');
      this.new();
      this.load();      
    });
  }

  remove() {
    if (confirm('Deseja realmente excluir registro')) {
      this.trilhaService.delete(this.trilha.id).subscribe(res => {
        window.alert('sucesso');
        this.new();
        this.load();
      });
    }
  }

}
