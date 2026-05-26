using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Exceptions;

namespace WizCo.Core.Domain.Entities;

public class Pedido : Entity
{
    private readonly List<ItemPedido> _itens = [];
    public string ClienteNome { get; private set; } = string.Empty;
    public DateTime DataCriacao { get; private set; }
    public StatusPedido Status { get; private set; }
    public decimal ValorTotal => _itens.Sum(x => x.ValorTotal);
    public IReadOnlyCollection<ItemPedido> Itens => _itens;

    // EF Core
    protected Pedido() { }

    public Pedido(string clienteNome, List<ItemPedido> itens)
    {
        Validar(clienteNome, itens);

        ClienteNome = clienteNome;
        DataCriacao = DateTime.UtcNow;
        Status = StatusPedido.Novo;

        _itens.AddRange(itens);
    }

    public void Cancelar()
    {
        DomainException.When(Status == StatusPedido.Pago, "Pedido pago não pode ser cancelado");
        DomainException.When(Status == StatusPedido.Cancelado, "Pedido já está cancelado");
        
        Status = StatusPedido.Cancelado;
    }

    public void MarcarComoPago()
    {
        DomainException.When(Status == StatusPedido.Cancelado, "Pedido cancelado não pode ser pago");
        Status = StatusPedido.Pago;
    }

    public void AdicionarItem(ItemPedido item)
    {
        DomainException.When(Status != StatusPedido.Novo, "Não é possível alterar pedidos finalizados");
        _itens.Add(item);
    }

    private static void Validar(string clienteNome, List<ItemPedido> itens)
    {
        DomainException.When(string.IsNullOrWhiteSpace(clienteNome), "Nome do cliente é obrigatório");
        DomainException.When(itens is null || itens.Count == 0, "Pedido deve possuir ao menos um item");
    }
}