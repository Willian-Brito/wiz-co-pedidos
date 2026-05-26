using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Enums;

namespace WizCo.Core.Application.UseCases.Pedidos.Queries;

public interface IPedidoQueries
{
    Task<Result<PagedResult<PedidoDTO>>> GetAll(int page, int pageSize, string query = null);
    Task<Result<PedidoDTO?>> GetByIdAsync(Guid id);
    Task<Result<PagedResult<PedidoDTO>>> GetByStatusAsync(StatusPedido? status, int page, int pageSize);
}