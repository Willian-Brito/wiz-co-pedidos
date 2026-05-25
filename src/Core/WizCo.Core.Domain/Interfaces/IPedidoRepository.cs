using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;

namespace WizCo.Core.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );

    Task<IEnumerable<Pedido>> GetByStatusAsync(
        StatusPedido? status,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default
    );

    Task<int> CountAsync(
        StatusPedido? status,
        CancellationToken cancellationToken = default
    );
}