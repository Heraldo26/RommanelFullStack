using Microsoft.EntityFrameworkCore;
using Rommanel.Domain.Cliente;
using Rommanel.Infrastructure.Context;
using Rommanel.Infrastructure.Repositories.Interfaces;

namespace Rommanel.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddClienteAsync(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteCpfOrEmailAsync(string documento, string email)
        {
            return await _context.Cliente
                .AnyAsync(c => c.Documento == documento || c.Email == email);
        }

        public async Task<Cliente> GetClienteIdAsync(int idCliente)
        {
            return await _context.Cliente
                .FirstOrDefaultAsync(c => c.IdCliente == idCliente);
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Cliente .ToListAsync();
        }

        public void EditarCliente(Cliente cliente)
        {
            _context.Update(cliente);
            _context.SaveChanges();
        }
        public void RemoveCliente(Cliente cliente)
        {
            _context.Remove(cliente);
            _context.SaveChanges();
        }
    }
}
