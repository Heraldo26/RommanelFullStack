using AutoMapper;
using MediatR;
using Rommanel.Application.Commands;
using Rommanel.Application.ViewModels;
using Rommanel.Domain;
using Rommanel.Domain.Cliente;
using Rommanel.Infrastructure.Repositories.Interfaces;
using System.Text.RegularExpressions;

namespace Rommanel.Application.Handlers
{
    public class CriarClienteCommandHandler : IRequestHandler<CriarClienteCommand, Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public CriarClienteCommandHandler(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<Cliente> Handle(CriarClienteCommand request, CancellationToken cancellationToken)
        {
            if (await _clienteRepository.ExisteCpfOrEmailAsync(request.Documento, request.Email))
                throw new DomainException("CPF/CNPJ ou E-mail já cadastrado.");

            request.Documento = Regex.Replace(request.Documento, "[^0-9]", "");

            var cliente = _mapper.Map<Cliente>(request);

            await _clienteRepository.AddClienteAsync(cliente);
            return cliente;
        }
    }
}
