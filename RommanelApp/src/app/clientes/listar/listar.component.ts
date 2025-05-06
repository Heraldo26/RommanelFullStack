import { Component, OnInit } from '@angular/core';
import { ClientesService } from '../../service/clientes.service';
import { ClienteDto } from '../../Interface/cliente.interface';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

@Component({
  selector: 'app-listar',
  templateUrl: './listar.component.html',
  styleUrl: './listar.component.scss',
  imports: [CommonModule, NgxMaskPipe],
  providers: [provideNgxMask()]
})
export class ListarComponent implements OnInit{
  
  clientes: ClienteDto[] = [];
  mostrarModalExcluir: boolean = false;
  clienteSelecionado: ClienteDto | undefined;

  constructor(private clientesService: ClientesService, private router: Router) {}

  ngOnInit(): void {
    this.carregarClientes();
  }

  carregarClientes(): void {
    this.clientesService.getClientes().subscribe({
      next: (dados) => {
        this.clientes = dados;
      },
      error: (err) => {
        console.error('Erro ao carregar clientes', err);
      }
    });
  }

  formularioCliente(cliente?: ClienteDto){
    if (cliente) {
      this.router.navigate([`/formulario-cliente/${cliente.idCliente}`]);
    } else {
      this.router.navigate(['/formulario-cliente']);
    }
  }

  abrirModalExcluir(cliente: ClienteDto){
    this.clienteSelecionado = cliente;
    this.mostrarModalExcluir = true;
  }

  fecharModalExcluir(){
    this.mostrarModalExcluir = false;
    this.clienteSelecionado = undefined;
  }

  excluirCliente(){
    if(this.clienteSelecionado){
      this.clientesService.excluir(this.clienteSelecionado.idCliente).subscribe(() => {
        this.carregarClientes();
        this.fecharModalExcluir();
      });
    }
  }
}
