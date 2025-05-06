using FluentValidation;
using Rommanel.Application.Commands;
using Rommanel.Application.Definitions;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Rommanel.Application.Validators
{
    public class ClienteBaseValidator<T> : AbstractValidator<T> where T : IClienteCommand
    {
        public ClienteBaseValidator()
        {
            RuleFor(c => c.Nome)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.");

            RuleFor(c => c.Documento)
                .NotEmpty().WithMessage("O campo Documento é obrigatório.")
                .Must(BeValidDocumento).WithMessage("O campo Documento deve ser um CPF ou CNPJ válido.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O campo Email é obrigatório.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")
                .WithMessage("O campo Email deve ser um endereço de e-mail válido.");

            RuleFor(c => c.TipoPessoa)
                .NotEmpty().WithMessage("O campo TipoPessoa é obrigatório.");

            RuleFor(c => c.Telefone)
                .NotEmpty().WithMessage("O campo Telefone é obrigatório.");

            RuleFor(c => c.DataNascimento)
                .NotEmpty().WithMessage("O campo DataNascimento é obrigatório.");

            RuleFor(c => c.Cep)
            .NotEmpty().WithMessage("O campo Cep é obrigatório.")
            .Length(8).WithMessage("O campo Cep deve ter 8 dígitos.");

            RuleFor(c => c.Rua).NotEmpty().WithMessage("O campo Rua é obrigatório.");
            RuleFor(c => c.Numero).NotEmpty().WithMessage("O campo Número é obrigatório.");
            RuleFor(c => c.Bairro).NotEmpty().WithMessage("O campo Bairro é obrigatório.");
            RuleFor(c => c.Cidade).NotEmpty().WithMessage("O campo Cidade é obrigatório.");
            RuleFor(c => c.Estado).NotEmpty().WithMessage("O campo Estado é obrigatório.");

            RuleFor(c => c)
            .Must(c =>
            {
                if (c.TipoPessoa == TipoPessoaEnum.Fisica.ToString())
                {
                    var idade = DateTime.Today.Year - c.DataNascimento.Year;
                    if (c.DataNascimento > DateTime.Today.AddYears(-idade)) idade--;
                    return idade >= 18;
                }

                return true;
            })
            .WithMessage("A idade mínima para pessoa física é 18 anos.");

            RuleFor(c => c)
            .Must(c =>
                c.TipoPessoa != TipoPessoaEnum.Juridica.ToString()
                || (!string.IsNullOrWhiteSpace(c.InscricaoEstadual) || c.IsentoIE)
            )
            .WithMessage("Para pessoa jurídica, informe a Inscrição Estadual ou marque como isento.");
        }

        private bool BeValidDocumento(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return false;

            documento = new string(documento.Where(char.IsDigit).ToArray());

            if (documento.Length == 11)
                return ValidaCpf(documento);

            if (documento.Length == 14)
                return ValidaCnpj(documento);

            return false;
        }

        private bool ValidaCpf(string cpf)
        {
            if (cpf.Distinct().Count() == 1) return false;

            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCpf = cpf.Substring(0, 9);
            var soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            tempCpf += digito1;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return cpf.EndsWith(digito1.ToString() + digito2.ToString());
        }

        private bool ValidaCnpj(string cnpj)
        {
            if (cnpj.Distinct().Count() == 1) return false;

            var multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var tempCnpj = cnpj.Substring(0, 12);
            var soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            var resto = soma % 11;
            var digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            var digito2 = resto < 2 ? 0 : 11 - resto;

            return cnpj.EndsWith(digito1.ToString() + digito2.ToString());
        }
    }
}
