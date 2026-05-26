using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;

namespace WizCo.Core.Domain.Interfaces;

public interface IPedidoRepository : IRepository<Pedido>
{
    Task<IEnumerable<Pedido>> GetAll(int pageSize, int pageIndex, string query = null);
    Task<Pedido?> GetByIdAsync(Guid id);
    Task<IEnumerable<Pedido>> GetByStatusAsync(StatusPedido? status, int page, int pageSize);
    Task<int> CountAsync(StatusPedido? status);
    
    /* ItemPedido */
    Task<ItemPedido> GetItemById(Guid id);
    Task<ItemPedido> GetItemByOrder(Guid orderId, Guid productId);
}