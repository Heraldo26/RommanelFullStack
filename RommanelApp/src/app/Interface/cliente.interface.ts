import { EnderecoDto } from "./endereco.interface";

export interface ClienteDto {
    idCliente: number;
    nome: string;
    documento: string;
    email: string;
    dataNascimento: string;
    telefone: string;
    tipoPessoa: string;
    inscricaoEstadual: string;
    isentoIE: boolean;
    endereco: EnderecoDto;
  }

