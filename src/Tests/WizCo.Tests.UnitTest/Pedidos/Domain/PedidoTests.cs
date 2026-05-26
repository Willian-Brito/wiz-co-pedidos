using FluentAssertions;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Exceptions;
using WizCo.Tests.UnitTest.Pedidos.Factories;

namespace WizCo.Tests.UnitTest.Pedidos.Domain;

public class PedidoTests
{
    [Fact]
    public void Deve_Criar_Pedido_Valido()
    {
        // Arrange
        var itens = new List<ItemPedido>
        {
            new("Notebook", 2, 3500)
        };

        // Act
        var pedido = new Pedido("Willian Brito", itens);

        // Assert
        pedido.ClienteNome.Should().Be("Willian Brito");
        pedido.Status.Should().Be(StatusPedido.Novo);
        pedido.Itens.Should().HaveCount(1);
        pedido.ValorTotal.Should().Be(7000);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_ClienteNome_For_Vazio()
    {
        // Arrange
        var itens = new List<ItemPedido>
        {
            new("Notebook", 1, 3000)
        };

        // Act
        Action act = () =>
            new Pedido(string.Empty, itens);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Nome do cliente é obrigatório");
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Pedido_Nao_Possuir_Itens()
    {
        // Arrange
        var itens = new List<ItemPedido>();

        // Act
        Action act = () =>
            new Pedido("Willian", itens);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido deve possuir ao menos um item");
    }

    [Fact]
    public void Deve_Cancelar_Pedido_Com_Status_Novo()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        // Act
        pedido.Cancelar();

        // Assert
        pedido.Status.Should().Be(StatusPedido.Cancelado);
    }

    [Fact]
    public void Nao_Deve_Cancelar_Pedido_Pago()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        pedido.MarcarComoPago();

        // Act
        Action act = () => pedido.Cancelar();

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido pago não pode ser cancelado");
    }

    [Fact]
    public void Nao_Deve_Cancelar_Pedido_Ja_Cancelado()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        pedido.Cancelar();

        // Act
        Action act = () => pedido.Cancelar();

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido já está cancelado");
    }

    [Fact]
    public void Deve_Marcar_Pedido_Como_Pago()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        // Act
        pedido.MarcarComoPago();

        // Assert
        pedido.Status.Should().Be(StatusPedido.Pago);
    }

    [Fact]
    public void Nao_Deve_Marcar_Pedido_Cancelado_Como_Pago()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        pedido.Cancelar();

        // Act
        Action act = () => pedido.MarcarComoPago();

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Pedido cancelado não pode ser pago");
    }

    [Fact]
    public void Deve_Adicionar_Item_Em_Pedido_Novo()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        var item = new ItemPedido(
            "Mouse",
            1,
            100);

        // Act
        pedido.AdicionarItem(item);

        // Assert
        pedido.Itens.Should().HaveCount(2);
        pedido.ValorTotal.Should().Be(3100);
    }

    [Fact]
    public void Nao_Deve_Adicionar_Item_Em_Pedido_Finalizado()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        pedido.MarcarComoPago();

        var item = new ItemPedido(
            "Mouse",
            1,
            100
        );

        // Act
        Action act = () => pedido.AdicionarItem(item);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Não é possível alterar pedidos finalizados");
    }
}