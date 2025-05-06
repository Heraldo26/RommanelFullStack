import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { ClienteDto } from '../Interface/cliente.interface';

@Injectable({
  providedIn: 'root'
})
export class ClientesService {
  private apiUrl = `${environment.apiUrl}/Clientes`;

  constructor(private http: HttpClient) { }

  getClientes(): Observable<ClienteDto[]> {
    return this.http.get<ClienteDto[]>(`${this.apiUrl}/ListarClientes`);
  }

  getCliente(idCliente: any): Observable<ClienteDto> {
    return this.http.get<ClienteDto>(`${this.apiUrl}/ObterClientePorId/${idCliente}`);
  }

  adicionar(cliente: ClienteDto): Observable<ClienteDto> {
    return this.http.post<ClienteDto>(`${this.apiUrl}/CriarCliente`, cliente);
  }

  atualizar(cliente: ClienteDto): Observable<ClienteDto> {
    return this.http.put<ClienteDto>(`${this.apiUrl}/AtualizarCliente`, cliente);
  }

  excluir(idCliente: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/DeletarCliente/${idCliente}`);
  }
}
