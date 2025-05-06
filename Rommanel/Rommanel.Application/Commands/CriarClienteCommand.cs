using MediatR;
using Rommanel.Application.Definitions;
using Rommanel.Domain.Cliente;

namespace Rommanel.Application.Commands
{
    public class CriarClienteCommand : IRequest<Cliente>, IClienteCommand
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string TipoPessoa { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }
    }
}
