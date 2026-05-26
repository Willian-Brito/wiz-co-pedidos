namespace WizCo.Core.Application.UseCases.Pedidos.DTOs;

public class PedidoDTO
{
    public Guid Id { get; set; }

    public string ClienteNome { get; set; } = string.Empty;

    public DateTime DataCriacao { get; set; }

    public string Status { get; set; } = string.Empty;

    public decimal ValorTotal { get; set; }

    public List<ItemPedidoDTO> Itens { get; set; } = [];
}