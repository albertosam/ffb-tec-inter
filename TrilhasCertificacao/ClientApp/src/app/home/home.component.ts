import { Component, OnInit } from '@angular/core';
import { CertificacaoService } from '../certificacao/certificacao.service';
import { TrilhaService } from '../certificacao/trilha.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  trilhas: any = [];
  certificacoes: any = [];

  constructor(private certificacaoService: CertificacaoService, private trilhaService: TrilhaService) { }

  ngOnInit(): void {
    this.certificacaoService.getAll().subscribe(res => this.certificacoes = res);
    this.trilhaService.getAll().subscribe(res => this.trilhas = res);
  }

}
