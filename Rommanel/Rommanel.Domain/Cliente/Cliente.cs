
using System.ComponentModel.DataAnnotations;

namespace Rommanel.Domain.Cliente
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public int IdEndereco { get; set; }
        public Endereco Endereco { get; set; }
        public string TipoPessoa { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }

    }
}
