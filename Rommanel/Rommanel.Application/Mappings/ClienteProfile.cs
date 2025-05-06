using AutoMapper;
using Rommanel.Application.Commands;
using Rommanel.Application.ViewModels;
using Rommanel.Domain.Cliente;

namespace Rommanel.Application.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteViewModel, AtualizarClienteCommand>();
            CreateMap<AtualizarClienteCommand, ClienteViewModel>();

            CreateMap<Cliente, ClienteViewModel>();
            CreateMap<ClienteViewModel, Cliente>();

            CreateMap<Endereco, EnderecoViewModel>();
            CreateMap<EnderecoViewModel, Endereco>();


            CreateMap<AtualizarClienteCommand, Cliente>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco
                {
                    Cep = src.Cep,
                    Rua = src.Rua,
                    Numero = src.Numero,
                    Bairro = src.Bairro,
                    Cidade = src.Cidade,
                    Estado = src.Estado
                }));
            
            CreateMap<CriarClienteCommand, Cliente>()
                .ForMember(dest => dest.Endereco, opt => opt.MapFrom(src => new Endereco
                {
                    Cep = src.Cep,
                    Rua = src.Rua,
                    Numero = src.Numero,
                    Bairro = src.Bairro,
                    Cidade = src.Cidade,
                    Estado = src.Estado
                }));

        }
    }
}
