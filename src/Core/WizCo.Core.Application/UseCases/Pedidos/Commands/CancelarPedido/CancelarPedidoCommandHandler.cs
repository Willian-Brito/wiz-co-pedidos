using MediatR;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Core.Application.UseCases.Pedidos.Commands.CancelarPedido;

public class CancelarPedidoCommandHandler  : CommandHandler, IRequestHandler<CancelarPedidoCommand, Result>
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelarPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result> Handle(CancelarPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(request.Id);

        if (pedido is null)
            return Result.Failure("Pedido não encontrado");

        pedido.Cancelar();
        _pedidoRepository.Update(pedido);
        await _unitOfWork.Commit();

        return Result.Ok();
    }
}