using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rommanel.Domain.Cliente;

namespace Rommanel.Infrastructure.Configurations
{
    public class ClienteConfigurations : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Primary Key
            builder.HasKey(t => t.IdCliente);

            // Properties
            builder.Property(t => t.Nome).IsRequired();
            builder.Property(t => t.Documento).IsRequired();
            builder.Property(t => t.Email).IsRequired();
            builder.Property(t => t.DataNascimento);
            builder.Property(t => t.Telefone);
            builder.Property(t => t.TipoPessoa);
            builder.Property(t => t.InscricaoEstadual);
            builder.Property(t => t.IsentoIE);

            // Table & Column Mappings
            builder.ToTable("Cliente");

            builder.Property(t => t.IdCliente).HasColumnName("ID_CLIENTE").UseIdentityColumn();
            builder.Property(t => t.Nome).HasColumnName("NOME");
            builder.Property(t => t.Documento).HasColumnName("DOCUMENTO");
            builder.Property(t => t.Email).HasColumnName("EMAIL");
            builder.Property(t => t.DataNascimento).HasColumnName("DATA_NASCIMENTO");
            builder.Property(t => t.Telefone).HasColumnName("TELEFONE");
            builder.Property(t => t.TipoPessoa).HasColumnName("TIPO_PESSOA");
            builder.Property(t => t.InscricaoEstadual).HasColumnName("INSCRICAO_ESTADUAL");
            builder.Property(t => t.IsentoIE).HasColumnName("INSETO_IE");

            builder.HasIndex(c => c.Documento).IsUnique();
            builder.HasIndex(c => c.Email).IsUnique();

            builder.HasOne(c => c.Endereco)
                .WithOne()
                .HasForeignKey<Cliente>(c => c.IdEndereco);

        }
    }
}
