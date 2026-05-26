using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;

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
        
        builder.Property(x => x.ValorTotal)
            .HasColumnType("DECIMAL(18,2)")
            .IsRequired();

        builder.HasMany(x => x.Itens)
            .WithOne()
            .HasForeignKey(x => x.PedidoId);
        
        #region Seed
        
        builder.HasData(
            new
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ClienteNome = "João Silva",
                DataCriacao = DateTime.UtcNow.AddDays(-20),
                Status = StatusPedido.Pago,
                ValorTotal = 4500m
            },
            new
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ClienteNome = "Maria Oliveira",
                DataCriacao = DateTime.UtcNow.AddDays(-18),
                Status = StatusPedido.Novo,
                ValorTotal = 7999m
            },
            new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ClienteNome = "Carlos Souza",
                DataCriacao = DateTime.UtcNow.AddDays(-17),
                Status = StatusPedido.Cancelado,
                ValorTotal = 3200m
            },
            new
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ClienteNome = "Fernanda Lima",
                DataCriacao = DateTime.UtcNow.AddDays(-15),
                Status = StatusPedido.Pago,
                ValorTotal = 4200m
            },
            new
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                ClienteNome = "Ricardo Mendes",
                DataCriacao = DateTime.UtcNow.AddDays(-14),
                Status = StatusPedido.Novo,
                ValorTotal = 1100m
            },
            new
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                ClienteNome = "Patrícia Gomes",
                DataCriacao = DateTime.UtcNow.AddDays(-13),
                Status = StatusPedido.Pago,
                ValorTotal = 1800m
            },
            new
            {
                Id = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                ClienteNome = "Lucas Ferreira",
                DataCriacao = DateTime.UtcNow.AddDays(-12),
                Status = StatusPedido.Cancelado,
                ValorTotal = 350m
            },
            new
            {
                Id = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                ClienteNome = "Juliana Costa",
                DataCriacao = DateTime.UtcNow.AddDays(-11),
                Status = StatusPedido.Novo,
                ValorTotal = 3200m
            },
            new
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                ClienteNome = "André Martins",
                DataCriacao = DateTime.UtcNow.AddDays(-10),
                Status = StatusPedido.Pago,
                ValorTotal = 1400m
            },
            new
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ClienteNome = "Camila Rocha",
                DataCriacao = DateTime.UtcNow.AddDays(-9),
                Status = StatusPedido.Novo,
                ValorTotal = 897m
            }
        );
        #endregion
    }
}