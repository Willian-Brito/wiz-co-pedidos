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

        #region Seed
        builder.HasData(
            new
            {
                Id = Guid.Parse("21111111-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ProdutoNome = "Notebook Dell Inspiron",
                Quantidade = 1,
                PrecoUnitario = 4500m
            },
            new
            {
                Id = Guid.Parse("22222222-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ProdutoNome = "iPhone 15 Pro",
                Quantidade = 1,
                PrecoUnitario = 7999m
            },
            new
            {
                Id = Guid.Parse("23333333-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ProdutoNome = "Smart TV Samsung 55",
                Quantidade = 1,
                PrecoUnitario = 3200m
            },
            new
            {
                Id = Guid.Parse("24444444-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ProdutoNome = "PlayStation 5",
                Quantidade = 1,
                PrecoUnitario = 4200m
            },
            new
            {
                Id = Guid.Parse("25555555-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                ProdutoNome = "Mouse Logitech MX Master 3",
                Quantidade = 2,
                PrecoUnitario = 550m
            },
            new
            {
                Id = Guid.Parse("26666666-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                ProdutoNome = "Monitor LG Ultrawide",
                Quantidade = 1,
                PrecoUnitario = 1800m
            },
            new
            {
                Id = Guid.Parse("27777777-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                ProdutoNome = "Teclado Mecânico Redragon",
                Quantidade = 1,
                PrecoUnitario = 350m
            },
            new
            {
                Id = Guid.Parse("28888888-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                ProdutoNome = "Apple Watch Series 9",
                Quantidade = 1,
                PrecoUnitario = 3200m
            },
            new
            {
                Id = Guid.Parse("29999999-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                ProdutoNome = "Cadeira Gamer ThunderX3",
                Quantidade = 1,
                PrecoUnitario = 1400m
            },
            new
            {
                Id = Guid.Parse("2aaaaaaa-1111-1111-1111-111111111111"),
                PedidoId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProdutoNome = "Echo Dot Alexa",
                Quantidade = 3,
                PrecoUnitario = 299m
            }
        );
        #endregion
    }
}