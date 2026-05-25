using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Exceptions;

namespace WizCo.Core.Domain.Entities;

public class ItemPedido : Entity
{
    public Guid PedidoId { get; private set; }

    public string ProdutoNome { get; private set; } = string.Empty;

    public int Quantidade { get; private set; }

    public decimal PrecoUnitario { get; private set; }

    public decimal ValorTotal => Quantidade * PrecoUnitario;

    // EF Core
    protected ItemPedido() { }

    public ItemPedido(string produtoNome, int quantidade, decimal precoUnitario)
    {
        Validar(produtoNome, quantidade, precoUnitario);

        ProdutoNome = produtoNome;
        Quantidade = quantidade;
        PrecoUnitario = precoUnitario;
    }

    private static void Validar(string produtoNome, int quantidade, decimal precoUnitario)
    {
        DomainException.When(string.IsNullOrWhiteSpace(produtoNome), "Nome do produto é obrigatório");
        DomainException.When(quantidade <= 0, "Quantidade deve ser maior que zero");
        DomainException.When(precoUnitario <= 0, "Preço unitário deve ser maior que zero");
    }
}