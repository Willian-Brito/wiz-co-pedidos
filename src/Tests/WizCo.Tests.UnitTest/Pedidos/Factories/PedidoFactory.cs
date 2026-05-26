using WizCo.Core.Domain.Entities;

namespace WizCo.Tests.UnitTest.Pedidos.Factories;

public static class PedidoFactory
{
    public static Pedido Criar()
    {
        return new Pedido(
            "Willian Brito",
            new List<ItemPedido>
            {
                new("Notebook", 1, 3000)
            });
    }
}