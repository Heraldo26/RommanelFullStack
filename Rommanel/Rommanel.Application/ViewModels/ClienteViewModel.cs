namespace Rommanel.Application.ViewModels
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string TipoPessoa { get; set; }
        public string InscricaoEstadual { get; set; }
        public bool IsentoIE { get; set; }

        public EnderecoViewModel Endereco { get; set; }
    }
}
