using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommanel.Domain.Cliente;

namespace Rommanel.Infrastructure.Configurations
{
    public class EnderecoConfigurations : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            // Primary Key
            builder.HasKey(t => t.IdEndereco);

            // Properties
            builder.Property(t => t.Cep).IsRequired();
            builder.Property(t => t.Rua).IsRequired();
            builder.Property(t => t.Numero).IsRequired();
            builder.Property(t => t.Bairro).IsRequired();
            builder.Property(t => t.Cidade).IsRequired();
            builder.Property(t => t.Estado).IsRequired();

            // Table & Column Mappings
            builder.ToTable("Endereco");

            builder.Property(t => t.IdEndereco).HasColumnName("ID_ENDERECO").UseIdentityColumn();
            builder.Property(t => t.Cep).HasColumnName("CEP");
            builder.Property(t => t.Rua).HasColumnName("RUA");
            builder.Property(t => t.Numero).HasColumnName("NUMERO");
            builder.Property(t => t.Bairro).HasColumnName("BAIRRO");
            builder.Property(t => t.Cidade).HasColumnName("CIDADE");
            builder.Property(t => t.Estado).HasColumnName("ESTADO");

        }
    }
}
