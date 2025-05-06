using MediatR;
using Rommanel.Application.ViewModels;

namespace Rommanel.Application.Queries
{
    public class ListarClientesQuery : IRequest<List<ClienteViewModel>>
    {
    }
}
