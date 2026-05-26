using FluentValidation;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;

namespace WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;

public class CriarPedidoCommand : Command<Result<PedidoDTO>>
{
    public string ClienteNome { get; init; } = string.Empty;

    public List<ItemPedidoDTO> Itens { get; init; } = [];
    
    public override bool Validar()
    {
        ValidationResult = new CriarPedidoValidator().Validate(this);
        return ValidationResult.IsValid;
    }
    
    public class CriarPedidoValidator : AbstractValidator<CriarPedidoCommand>
    {
        public CriarPedidoValidator()
        {
            RuleFor(x => x.ClienteNome)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Itens)
                .NotEmpty()
                .WithMessage("Pedido deve possuir itens");

            RuleForEach(x => x.Itens)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProdutoNome)
                        .NotEmpty()
                        .MaximumLength(150);

                    item.RuleFor(i => i.Quantidade)
                        .GreaterThan(0);

                    item.RuleFor(i => i.PrecoUnitario)
                        .GreaterThan(0);
                });
        }
    }
}