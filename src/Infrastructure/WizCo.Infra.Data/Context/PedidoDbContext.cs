using Microsoft.EntityFrameworkCore;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Infra.Data.Context;

public class PedidoDbContext : DbContext, IUnitOfWork
{
    public DbSet<Pedido> Pedidos => Set<Pedido>();

    public DbSet<ItemPedido> ItensPedido => Set<ItemPedido>();
    
    public PedidoDbContext(DbContextOptions<PedidoDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PedidoDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
    
    public async Task<bool> Commit()
    {
        return await base.SaveChangesAsync() > 0;
    }
}