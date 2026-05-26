using AutoMapper;
using MediatR;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;

public sealed class CriarPedidoCommandHandler : CommandHandler, IRequestHandler<CriarPedidoCommand, Result<PedidoDTO>>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CriarPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<PedidoDTO>> Handle(CriarPedidoCommand message, CancellationToken cancellationToken)
    {
        var itens = message.Itens
            .Select(x => new ItemPedido(
                x.ProdutoNome,
                x.Quantidade,
                x.PrecoUnitario)
            )
            .ToList();

        var pedido = new Pedido(message.ClienteNome, itens);

        await _pedidoRepository.AddAsync(pedido);
        await _unitOfWork.Commit();
        var response = _mapper.Map<PedidoDTO>(pedido);
        
        return Result<PedidoDTO>.Ok(response);
    }
}