

using Rommanel.Domain.Cliente;

namespace Rommanel.Infrastructure.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<bool> ExisteCpfOrEmailAsync(string documento, string email);
        Task AddClienteAsync(Cliente cliente);
        Task<Cliente> GetClienteIdAsync(int idCliente);
        Task<List<Cliente>> GetAllAsync();
        void RemoveCliente(Cliente cliente);
        void EditarCliente(Cliente cliente);
    }
}
