using MediatR;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Core.Application.UseCases.Pedidos.Commands.PagarPedido;

public class PagarPedidoCommandHandler : CommandHandler, IRequestHandler<PagarPedidoCommand, Result>
{
    private readonly IPedidoRepository _pedidoRepository;

    private readonly IUnitOfWork _unitOfWork;

    public PagarPedidoCommandHandler(IPedidoRepository pedidoRepository, IUnitOfWork unitOfWork)
    {
        _pedidoRepository = pedidoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(PagarPedidoCommand request, CancellationToken cancellationToken)
    {
        var pedido = await _pedidoRepository.GetByIdAsync(request.Id);

        if (pedido is null)
            return Result.Failure("Pedido não encontrado");

        pedido.MarcarComoPago();
        _pedidoRepository.Update(pedido);

        await _unitOfWork.Commit();

        return Result.Ok();
    }
}