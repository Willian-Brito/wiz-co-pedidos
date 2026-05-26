using FluentValidation;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;

namespace WizCo.Core.Application.UseCases.Pedidos.Commands.PagarPedido;

public class PagarPedidoCommand : Command<Result>
{
    public Guid Id { get; init; }

    public PagarPedidoCommand(Guid id)
    {
        Id = id;
    }

    public override bool Validar()
    {
        ValidationResult = new PagarPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }

    public class PagarPedidoValidator : AbstractValidator<PagarPedidoCommand>
    {
        public PagarPedidoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id do pedido é obrigatório");
        }
    }
}