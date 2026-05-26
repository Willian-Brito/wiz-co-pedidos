using System.Text.Json.Serialization;

namespace WizCo.Core.Application.UseCases.Pedidos.DTOs;

public class ItemPedidoDTO
{
    public Guid Id { get; set; }

    public string ProdutoNome { get; set; } = string.Empty;

    public int Quantidade { get; set; }

    public decimal PrecoUnitario { get; set; }

    [JsonIgnore]
    public decimal ValorTotal => Quantidade * PrecoUnitario; 
}