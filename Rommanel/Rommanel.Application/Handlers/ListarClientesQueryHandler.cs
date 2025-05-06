using AutoMapper;
using MediatR;
using Rommanel.Application.Queries;
using Rommanel.Application.ViewModels;
using Rommanel.Infrastructure.Repositories.Interfaces;

namespace Rommanel.Application.Handlers
{
    public class ListarClientesQueryHandler : IRequestHandler<ListarClientesQuery, List<ClienteViewModel>>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ListarClientesQueryHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<List<ClienteViewModel>> Handle(ListarClientesQuery request, CancellationToken cancellationToken)
        {
            var clientes = await _clienteRepository.GetAllAsync();

            return _mapper.Map<List<ClienteViewModel>>(clientes);
        }
    }
}
