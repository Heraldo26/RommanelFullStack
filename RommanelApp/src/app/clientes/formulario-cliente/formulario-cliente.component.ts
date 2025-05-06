import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ClienteDto } from '../../Interface/cliente.interface';
import { FormsModule, NgForm } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ClientesService } from '../../service/clientes.service';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';

@Component({
  selector: 'app-formulario-cliente',
  templateUrl: './formulario-cliente.component.html',
  styleUrl: './formulario-cliente.component.scss',
  imports: [FormsModule, CommonModule, NgxMaskDirective],
  providers: [provideNgxMask()]
})
export class FormularioClienteComponent implements OnInit{
  @Input() clienteSelecionado: ClienteDto = {
    idCliente: 0,
    nome: '',
    email: '',
    documento: '',
    dataNascimento: '',
    telefone: '',
    tipoPessoa: '',
    endereco: {
      idEndereco: 0,
      rua: '',
      numero: '',
      bairro: '',
      cidade: '',
      estado: '',
      cep: ''
    },
    inscricaoEstadual: '',
    isentoIE: false
  };

  /**
   *
   */
  constructor(private clientesService: ClientesService, private route: ActivatedRoute, public router: Router) 
  {
  }

  ngOnInit() {
    const idCliente = this.route.snapshot.paramMap.get('idCliente');
    if (idCliente) {
      this.clientesService.getCliente(idCliente).subscribe(cliente => {
        console.log("cliente", cliente);
        this.clienteSelecionado = cliente;

        if (this.clienteSelecionado.dataNascimento) {
          this.clienteSelecionado.dataNascimento = new Date(this.clienteSelecionado.dataNascimento).toISOString().split('T')[0];
        }
      });
    }
  }

  verificarTipoPessoa() {
    if (this.clienteSelecionado.tipoPessoa === 'Fisica') {
      this.clienteSelecionado.inscricaoEstadual = '';
      this.clienteSelecionado.isentoIE = false;
    }
  }

  verificarIsencaoIE() {
    if (this.clienteSelecionado.isentoIE) {
      this.clienteSelecionado.inscricaoEstadual = '';
    }
  }

  salvarCliente(form: NgForm){
    if (form.invalid) {
      const mensagensErro: string[] = [];
      Object.keys(form.controls).forEach(key => {
        const controlErrors = form.controls[key].errors;
        
        if (controlErrors) {
          console.log(`Campo inválido: ${key}`, controlErrors);
          mensagensErro.push(`Campo inválido: ${key}`);
        }

      });

      alert(mensagensErro.join('\n'));
      return;
    }

    if(this.clienteSelecionado.tipoPessoa === 'Fisica'){
     const idade = this.calcularIdade(this.clienteSelecionado.dataNascimento);
     if(idade < 18){
      alert('Cliente menor de 18 anos');
      return;
     }
    }

    const cliente = {
      ...this.clienteSelecionado,
      ...this.clienteSelecionado.endereco,
    };

    if(this.clienteSelecionado.idCliente){
      this.clientesService.atualizar(cliente).subscribe({
        next: (res) => {
          alert('Cliente atualizado com sucesso');
          this.router.navigate(['/clientes']);
        },
        error: (err) => {
          console.error('Erro ao salvar cliente', err);

          if (err.error && err.error.errors) {
            const mensagens = Object.values(err.error.errors)
              .flat()
              .join('\n');

            alert('Erros de validação:\n' + mensagens);
          } else if (err.error && err.error.message) {
            alert('Erro: ' + err.error.message);
          
          } else if (err.error && err.error.erro) {
            alert('Erro: ' + err.error.erro);
          
          } else {
            alert('Erro desconhecido ao salvar cliente.');
          }
        }
      });

    }else{
      this.clientesService.adicionar(cliente).subscribe({
        next: (res) => {
          alert('Cliente cadastrado com sucesso');
          this.router.navigate(['/clientes'])
        },
        error: (err) => {
          console.error('Erro ao salvar cliente', err);

  if (err.error && err.error.errors) {
        const mensagens = Object.values(err.error.errors).flat().join('\n');
          alert('Erros de validação:\n' + mensagens);

      } else if (err.error && err.error.message) {
            alert('Erro: ' + err.error.message);
          
          } else if (err.error && err.error.erro) {
            alert('Erro: ' + err.error.erro);
          
          } else {
            alert('Erro desconhecido ao salvar cliente.');
          }
        }
      });
    }
  }

  calcularIdade(dataNascimento: string): number {
    const dataNascimentoDate = new Date(dataNascimento);
    const hoje = new Date();
    const idade = hoje.getFullYear() - dataNascimentoDate.getFullYear();
    return idade;
  }
}
