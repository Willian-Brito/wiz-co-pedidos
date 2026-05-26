using AutoMapper;
using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Core.Application.UseCases.Pedidos.Queries;

public class PedidoQueries : IPedidoQueries
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;

    public PedidoQueries(IPedidoRepository pedidoRepository, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<PedidoDTO>>> GetAllPagedAsync(int page, int pageSize)
    {
        var pedidos = _pedidoRepository.GetAllPaged(page, pageSize);
        var response = _mapper.Map<PagedResult<PedidoDTO>>(pedidos);

        return Result<PagedResult<PedidoDTO>>.Ok(response);
    }

    public async Task<Result<PedidoDTO>> GetByIdAsync(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);

        if (pedido is null)
            throw new Exception("Pedido não encontrado");
        
        var response = _mapper.Map<PedidoDTO>(pedido);
        
        return Result<PedidoDTO>.Ok(response);
    }

    public async Task<Result<PagedResult<PedidoDTO>>> GetByStatusPagedAsync(StatusPedido? status, int page, int pageSize)
    {
        var pedidos = await _pedidoRepository.GetByStatusAsync(status, page, pageSize);
        var response = _mapper.Map<PagedResult<PedidoDTO>>(pedidos);
        
        return Result<PagedResult<PedidoDTO>>.Ok(response); 
    }
}