using MediatR;
using Rommanel.Application.ViewModels;

namespace Rommanel.Application.Queries
{
    public class ObterClientePorIdQuery : IRequest<ClienteViewModel>
    {
        public int IdCliente { get; set; }

        public ObterClientePorIdQuery(int idCliente)
        {
            IdCliente = idCliente;
        }
    }
}
