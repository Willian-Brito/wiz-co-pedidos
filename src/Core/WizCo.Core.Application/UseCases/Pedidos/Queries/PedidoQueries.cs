using AutoMapper;
using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Core.Application.UseCases.Pedidos.Queries;

public sealed class PedidoQueries : IPedidoQueries
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IMapper _mapper;

    public PedidoQueries(IPedidoRepository pedidoRepository, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<PedidoDTO>>> GetAllPaged(int page, int pageSize)
    {
        var pedidos = await _pedidoRepository.GetAllAsync();
        var pedidosDTO = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
        var response = pedidosDTO.ToPagedList(page, pageSize);

        return Result<PagedResult<PedidoDTO>>.Ok(response);
    }

    public async Task<Result<PedidoDTO>> GetByIdAsync(Guid id)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(id);

        if (pedido is null)
            return Result<PedidoDTO>.Failure("Pedido não encontrado");
        
        var response = _mapper.Map<PedidoDTO>(pedido);
        
        return Result<PedidoDTO>.Ok(response);
    }

    public async Task<Result<PagedResult<PedidoDTO>>> GetByStatusPaged(StatusPedido? status, int page, int pageSize)
    {
        var pedidos = await _pedidoRepository.GetByStatusAsync(status, page, pageSize);
        var pedidosDto = _mapper.Map<IEnumerable<PedidoDTO>>(pedidos);
        var response = pedidosDto.ToPagedList(page, pageSize);
        
        return Result<PagedResult<PedidoDTO>>.Ok(response); 
    }
}