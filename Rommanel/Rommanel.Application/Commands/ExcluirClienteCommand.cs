using MediatR;

namespace Rommanel.Application.Commands
{
    public class ExcluirClienteCommand : IRequest<bool>
    {
        public int IdCliente { get; set; }

        public ExcluirClienteCommand(int idCliente)
        {
            IdCliente = idCliente;
        }
    }
}
