using FluentAssertions;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Exceptions;

namespace WizCo.Tests.UnitTest.Pedidos.Domain;

public class ItemPedidoTests
{
    [Fact]
    public void Deve_Criar_ItemPedido_Valido()
    {
        // Arrange & Act
        var itemPedido = new ItemPedido("Notebook", 2, 3500);

        // Assert
        itemPedido.ProdutoNome.Should().Be("Notebook");
        itemPedido.Quantidade.Should().Be(2);
        itemPedido.PrecoUnitario.Should().Be(3500);
        itemPedido.ValorTotal.Should().Be(7000);
    }

    [Fact]
    public void Deve_Calcular_ValorTotal_Corretamente()
    {
        // Arrange & Act
        var itemPedido = new ItemPedido("Mouse", 3, 150);

        // Assert
        itemPedido.ValorTotal.Should()
            .Be(450);
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_ProdutoNome_For_Vazio()
    {
        // Arrange & Act
        Action act = () => new ItemPedido(string.Empty, 1, 100);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Nome do produto é obrigatório");
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_ProdutoNome_For_Nulo()
    {
        // Arrange & Act
        Action act = () => new ItemPedido(null!, 1, 100);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Nome do produto é obrigatório");
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Quantidade_For_Zero()
    {
        // Arrange & Act
        Action act = () => new ItemPedido("Notebook", 0, 100);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Quantidade deve ser maior que zero");
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_Quantidade_For_Menor_Que_Zero()
    {
        // Arrange & Act
        Action act = () => new ItemPedido("Notebook", -1, 100);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Quantidade deve ser maior que zero");
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_PrecoUnitario_For_Zero()
    {
        // Arrange & Act
        Action act = () => new ItemPedido("Notebook", 1, 0);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Preço unitário deve ser maior que zero");
    }

    [Fact]
    public void Deve_Lancar_Excecao_Quando_PrecoUnitario_For_Menor_Que_Zero()
    {
        // Arrange & Act
        Action act = () => new ItemPedido("Notebook", 1, -10);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Preço unitário deve ser maior que zero");
    }
}