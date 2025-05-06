using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using Rommanel.Application.Commands;
using Rommanel.Api;
using System.Text;
using Xunit;
using Rommanel.Tests.Model;
using Rommanel.Domain.Cliente;
using Newtonsoft.Json.Linq;
using Azure;
using Rommanel.Tests.Helper;

namespace Rommanel.Tests.Integration
{
    public class CriarClienteCommandIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public CriarClienteCommandIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Deve_criar_cliente_com_dados_validos()
        {
            var novoCliente = new
            {
                Nome = "Giovanna Guimalhaes",
                Documento = GerarCpfValido.GerarCpf(),
                DataNascimento = "1990-01-01",
                Email = $"giovanna{Guid.NewGuid().ToString("N").Substring(0, 3)}@email.com",
                Telefone = "11988888888",
                Cep = "98765432",
                Rua = "Rua das Acácias",
                Numero = "100",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                TipoPessoa = "Fisica",
                inscricaoEstadual = "",
                isentoIE = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(novoCliente), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Clientes/CriarCliente", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Status: " + response.StatusCode);
            Console.WriteLine("Resposta: " + responseString);

            Assert.True(response.IsSuccessStatusCode, "A criação do cliente falhou. Código: " + response.StatusCode);
        }

        [Fact]
        public async Task Deve_retornar_erro_quando_cpf_ja_existir()
        {
            var clienteExistente = new
            {
                Nome = "Carlos Silva",
                Documento = "23759507042",
                DataNascimento = "1990-01-01",
                Email = "carlos@email.com",
                Telefone = "11988888888",
                Cep = "98765432",
                Rua = "Rua das Acácias",
                Numero = "100",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                TipoPessoa = "Fisica",
                inscricaoEstadual = "",
                isentoIE = false
            };

            var content = new StringContent(JsonConvert.SerializeObject(clienteExistente), Encoding.UTF8, "application/json");
            await _client.PostAsync("/api/Clientes/CriarCliente", content); //primeiro post para criar
            var response = await _client.PostAsync("/api/Clientes/CriarCliente", content);

            Assert.False(response.IsSuccessStatusCode);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Resposta de erro: " + responseString);

            var json = JObject.Parse(responseString);
            Assert.True(json.ContainsKey("erro"), "A resposta não contém a chave 'erro'.");
            Assert.Equal("CPF/CNPJ ou E-mail já cadastrado.", json["erro"]?.ToString());
        }

        [Fact]
        public async Task Deve_listar_todos_os_clientes()
        {
            var getResponse = await _client.GetAsync("/api/Clientes/ListarClientes");
            var responseString = await getResponse.Content.ReadAsStringAsync();

            Console.WriteLine("Resposta de listagem: " + responseString);
            Assert.True(getResponse.IsSuccessStatusCode, $"Erro ao listar clientes. Status: {getResponse.StatusCode}, Conteúdo: {responseString}");

            var clientes = JsonConvert.DeserializeObject<List<ClienteResponse>>(responseString);
            Assert.NotNull(clientes);
            Assert.NotEmpty(clientes);
        }

        [Fact]
        public async Task Deve_atualizar_cliente_existente()
        {
            
            var getResponse = await _client.GetAsync("/api/Clientes/ListarClientes");
            var responseString = await getResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Lista de clientes: " + responseString);

            var clientes = JsonConvert.DeserializeObject<List<ClienteResponse>>(responseString);

            Assert.NotNull(clientes);
            Assert.NotEmpty(clientes);

            var cliente = clientes.FirstOrDefault();
            Assert.NotNull(cliente);

            var clienteId = cliente.IdCliente.ToString();
            Assert.False(string.IsNullOrEmpty(clienteId));

            var clienteAtualizado = new
            {
                IdCliente = cliente.IdCliente,
                Nome = "Ana Souza Atualizada",
                Documento = cliente.Documento,
                DataNascimento = "1992-08-10",
                Email = cliente.Email,
                Telefone = "11966666666",
                Cep = "12345678",
                Rua = "Rua Inicial",
                Numero = "20",
                Bairro = "Centro",
                Cidade = "São Paulo",
                Estado = "SP",
                TipoPessoa = "Fisica",
                inscricaoEstadual = "",
                isentoIE = false
            };

            var putContent = new StringContent(JsonConvert.SerializeObject(clienteAtualizado), Encoding.UTF8, "application/json");

            var putResponse = await _client.PutAsync("/api/Clientes/AtualizarCliente", putContent);
            var putResponseContent = await putResponse.Content.ReadAsStringAsync();

            Console.WriteLine($"Status: {putResponse.StatusCode}");
            Console.WriteLine($"Resposta da atualização: {putResponseContent}");

            Assert.True(putResponse.IsSuccessStatusCode, $"A atualização do cliente falhou. Status: {putResponse.StatusCode}. Conteúdo: {putResponseContent}");
        }

        [Fact]
        public async Task Deve_excluir_cliente_existente()
        {            
            var getResponse = await _client.GetAsync("/api/Clientes/ListarClientes");
            var responseString = await getResponse.Content.ReadAsStringAsync();
            Console.WriteLine("Lista de clientes: " + responseString);

            var clientes = JsonConvert.DeserializeObject<List<ClienteResponse>>(responseString);
            Assert.NotNull(clientes);
            Assert.NotEmpty(clientes);

            var clienteCriado = clientes.FirstOrDefault();
            Assert.NotNull(clienteCriado);

            var idCliente = clienteCriado.IdCliente.ToString();
            Assert.False(string.IsNullOrEmpty(idCliente));

            var deleteResponse = await _client.DeleteAsync($"/api/Clientes/DeletarCliente/{idCliente}");
            Assert.True(deleteResponse.IsSuccessStatusCode, $"A exclusão do cliente falhou. Status: {deleteResponse.StatusCode}");

            var getAfterDelete = await _client.GetAsync("/api/Clientes/ListarClientes");
            var responseAfterDelete = await getAfterDelete.Content.ReadAsStringAsync();
            Console.WriteLine("Lista após exclusão: " + responseAfterDelete);

            Assert.True(getAfterDelete.IsSuccessStatusCode, $"Falha ao listar clientes após exclusão. Status: {getAfterDelete.StatusCode}");

            var clientesAposExclusao = JsonConvert.DeserializeObject<List<ClienteResponse>>(responseAfterDelete);
            Assert.NotNull(clientesAposExclusao);

            Assert.DoesNotContain(clientesAposExclusao, c => c.IdCliente.ToString() == idCliente);
        }
    }
}
