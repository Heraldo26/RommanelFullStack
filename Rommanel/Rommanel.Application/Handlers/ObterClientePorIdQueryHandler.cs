using AutoMapper;
using MediatR;
using Rommanel.Application.Queries;
using Rommanel.Application.ViewModels;
using Rommanel.Domain.Cliente;
using Rommanel.Infrastructure.Repositories.Interfaces;

namespace Rommanel.Application.Handlers
{
    public class ObterClientePorIdQueryHandler : IRequestHandler<ObterClientePorIdQuery, ClienteViewModel>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ObterClientePorIdQueryHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<ClienteViewModel> Handle(ObterClientePorIdQuery request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetClienteIdAsync(request.IdCliente);

            if (cliente == null)
                return null;
            
            return _mapper.Map<ClienteViewModel>(cliente);            
        }
    }
}
