using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;

namespace WizCo.Core.Domain.Interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<IEnumerable<Pedido>> GetAllAsync();
    PagedResult<Pedido> GetAllPaged(int pageSize, int pageIndex);
    Task<Pedido?> GetByIdAsync(Guid id);
    Task<IEnumerable<Pedido>> GetByStatusAsync(StatusPedido? status, int page, int pageSize);
    PagedResult<Pedido> GetByStatusPaged(StatusPedido? status, int page, int pageSize);
    Task<int> CountAsync(StatusPedido? status);
    
    /* ItemPedido */
    Task<ItemPedido> GetItemById(Guid id);
    Task<ItemPedido> GetItemByOrder(Guid pedidoId);
}