using AutoMapper;
using MediatR;
using Rommanel.Application.Commands;
using Rommanel.Application.ViewModels;
using Rommanel.Domain;
using Rommanel.Domain.Cliente;
using Rommanel.Infrastructure.Repositories.Interfaces;

namespace Rommanel.Application.Handlers
{
    public class AtualizarClienteCommandHandler : IRequestHandler<AtualizarClienteCommand, ClienteViewModel>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public AtualizarClienteCommandHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<ClienteViewModel> Handle(AtualizarClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetClienteIdAsync(request.IdCliente);

            if (cliente == null)
                throw new DomainException("Cliente não encontrado.");

            _mapper.Map(request, cliente);

            _clienteRepository.EditarCliente(cliente);

            return _mapper.Map<ClienteViewModel>(cliente);
        }
    }
}
