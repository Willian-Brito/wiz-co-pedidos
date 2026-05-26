using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizCo.Core.Domain.Entities;

namespace WizCo.Infra.Data.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ClienteNome)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(x => x.DataCriacao)
            .IsRequired();

        builder.Ignore(x => x.ValorTotal);

        builder.HasMany(x => x.Itens)
            .WithOne()
            .HasForeignKey(x => x.PedidoId);
    }
}