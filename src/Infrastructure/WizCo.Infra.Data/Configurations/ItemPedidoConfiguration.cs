using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizCo.Core.Domain.Entities;

namespace WizCo.Infra.Data.Configurations;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ProdutoNome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Quantidade)
            .IsRequired();

        builder.Property(x => x.PrecoUnitario)
            .HasColumnType("decimal(18,2)");

        builder.Ignore(x => x.ValorTotal);
    }
}