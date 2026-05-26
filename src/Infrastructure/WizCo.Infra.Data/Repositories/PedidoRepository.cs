using Microsoft.EntityFrameworkCore;
using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Interfaces;
using WizCo.Infra.Data.Context;

namespace WizCo.Infra.Data.Repositories;

public sealed class PedidoRepository : IPedidoRepository
{
    public IUnitOfWork UnitOfWork => _context;
    private readonly PedidoDbContext _context;

    public PedidoRepository(PedidoDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Pedido entity)
    {
        await _context.Pedidos.AddAsync(entity);
    }

    public void Update(Pedido entity)
    {
        _context.Pedidos.Update(entity);
    }

    public void Remove(Pedido entity)
    {
        _context.Pedidos.Remove(entity);
    }

    public async Task<IEnumerable<Pedido>> GetAllAsync()
    {
        return await _context.Pedidos
            .Include(x => x.Itens)
            .OrderByDescending(x => x.DataCriacao)
            .AsNoTracking()
            .ToListAsync();
    }

    public PagedResult<Pedido> GetAllPaged(int pageSize, int pageIndex)
    {
        var query = _context.Pedidos
            .Include(x => x.Itens)
            .OrderByDescending(x => x.DataCriacao)
            .AsNoTracking();
        
        return query.ToPagedList();
    }

    public async Task<Pedido?> GetByIdAsync(Guid id)
    {
        return await _context.Pedidos
            .Include(x => x.Itens)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<Pedido>> GetByStatusAsync(StatusPedido? status, int page, int pageSize)
    {
        var query = _context.Pedidos
            .Include(x => x.Itens)
            .OrderByDescending(x => x.DataCriacao)
            .AsNoTracking();

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);
        
        return await query.ToListAsync();
    }

    public PagedResult<Pedido> GetByStatusPaged(StatusPedido? status, int page, int pageSize)
    {
        var query = _context.Pedidos
            .Include(x => x.Itens)
            .OrderByDescending(x => x.DataCriacao)
            .AsNoTracking();

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);
        
        return query.ToPagedList();
    }

    public async Task<int> CountAsync(StatusPedido? status)
    {
        IQueryable<Pedido> query = _context.Pedidos;

        if (status.HasValue)
            query = query.Where(x => x.Status == status.Value);

        return await query.CountAsync();
    }

    public async Task<ItemPedido> GetItemById(Guid id)
    {
        return await _context.ItensPedido.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<ItemPedido> GetItemByOrder(Guid pedidoId)
    {
        return await _context.ItensPedido.FirstOrDefaultAsync(i => i.PedidoId == pedidoId);
    }
}