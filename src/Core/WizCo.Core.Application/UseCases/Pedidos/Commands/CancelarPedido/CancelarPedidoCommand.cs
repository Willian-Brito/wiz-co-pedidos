using FluentValidation;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;

namespace WizCo.Core.Application.UseCases.Pedidos.Commands.CancelarPedido;

public class CancelarPedidoCommand : Command<Result>
{
    public Guid Id { get; init; }

    public CancelarPedidoCommand(Guid id)
    {
        Id = id;
    }

    public override bool Validar()
    {
        ValidationResult = new CancelarPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
    
    public class CancelarPedidoValidator : AbstractValidator<CancelarPedidoCommand>
    {
        public CancelarPedidoValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id do pedido é obrigatório");
        }
    }
}