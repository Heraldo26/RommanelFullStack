using MediatR;
using Rommanel.Application.Commands;
using Rommanel.Domain;
using Rommanel.Infrastructure.Repositories.Interfaces;

namespace Rommanel.Application.Handlers
{
    public class ExcluirClienteCommandHandler : IRequestHandler<ExcluirClienteCommand, bool>
    {
        private readonly IClienteRepository _clienteRepository;

        public ExcluirClienteCommandHandler(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<bool> Handle(ExcluirClienteCommand request, CancellationToken cancellationToken)
        {
            var cliente = await _clienteRepository.GetClienteIdAsync(request.IdCliente);

            if (cliente == null)            
                throw new DomainException($"Cliente com ID {request.IdCliente} não encontrado.");            

            _clienteRepository.RemoveCliente(cliente);

            return true;
        }
    }
}
