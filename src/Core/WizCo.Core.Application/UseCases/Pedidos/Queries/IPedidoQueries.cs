using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Enums;

namespace WizCo.Core.Application.UseCases.Pedidos.Queries;

public interface IPedidoQueries
{
    Task<Result<PagedResult<PedidoDTO>>> GetAllPagedAsync(int page, int pageSize);
    Task<Result<PedidoDTO?>> GetByIdAsync(Guid id);
    Task<Result<PagedResult<PedidoDTO>>> GetByStatusPagedAsync(StatusPedido? status, int page, int pageSize);
}