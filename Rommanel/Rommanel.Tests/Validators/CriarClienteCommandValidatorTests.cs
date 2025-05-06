using FluentAssertions;
using Rommanel.Application.Commands;
using Rommanel.Application.Validators;
using Xunit;

namespace Rommanel.Tests.Validators
{
    public class CriarClienteCommandValidatorTests
    {
        private readonly CriarClienteCommandValidator _validator;

        public CriarClienteCommandValidatorTests()
        {
            _validator = new CriarClienteCommandValidator();
        }

        [Fact]
        public void Deve_retornar_erro_se_nome_estiver_vazio()
        {
            var command = new CriarClienteCommand { Nome = "" };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Nome");
        }

        [Fact]
        public void Deve_retornar_erro_para_documento_invalido()
        {
            var command = new CriarClienteCommand { Documento = "12345678900" };

            var result = _validator.Validate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.PropertyName == "Documento");
        }

        [Fact]
        public void Deve_retornar_erro_para_email_invalido()
        {
            var command = new CriarClienteCommand { Email = "emailinvalido" };

            var resultado = _validator.Validate(command);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.PropertyName == "Email");
        }

        [Fact]
        public void Deve_retornar_erro_para_pessoa_fisica_menor_de_18_anos()
        {
            var command = new CriarClienteCommand
            {
                TipoPessoa = "Fisica",
                DataNascimento = DateTime.Today.AddYears(-17)
            };

            var resultado = _validator.Validate(command);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage.Contains("idade mínima"));
        }

        [Fact]
        public void Deve_retornar_erro_para_pessoa_juridica_sem_inscricao_estadual_e_nao_isento()
        {
            var command = new CriarClienteCommand
            {
                TipoPessoa = "Juridica",
                InscricaoEstadual = "",
                IsentoIE = false
            };

            var resultado = _validator.Validate(command);

            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().Contain(e => e.ErrorMessage.Contains("Inscrição Estadual"));
        }

        [Fact]
        public void Deve_passar_com_dados_validos()
        {
            var command = new CriarClienteCommand
            {
                Nome = "João da Silva",
                Documento = "58332281075",
                Email = $"joao{Guid.NewGuid()}@email.com",
                TipoPessoa = "Fisica",
                Telefone = "11999999999",
                DataNascimento = DateTime.Parse("01/01/1990 00:00:00"),
                Cep = "12345678",
                Rua = "Rua 25 de março",
                Numero = "123",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                InscricaoEstadual = "",
                IsentoIE = false
            };

            var resultado = _validator.Validate(command);

            resultado.IsValid.Should().BeTrue();
        }
    }
}
