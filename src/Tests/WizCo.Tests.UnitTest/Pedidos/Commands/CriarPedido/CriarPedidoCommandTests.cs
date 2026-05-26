using FluentAssertions;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;

namespace WizCo.Tests.UnitTest.Pedidos.Commands.CriarPedido;

public class CriarPedidoCommandTests
{
    [Fact]
    public void Deve_Retornar_Erro_Quando_ClienteNome_For_Vazio()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = string.Empty,
            Itens =
            [
                new ItemPedidoDTO
                {
                    ProdutoNome = "Notebook",
                    Quantidade = 1,
                    PrecoUnitario = 3000
                }
            ]
        };

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeFalse();

        command.ValidationResult.Errors
            .Should()
            .Contain(x => x.PropertyName == "ClienteNome");
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Nao_Possuir_Itens()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens = []
        };

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeFalse();

        command.ValidationResult.Errors
            .Should()
            .Contain(x => x.ErrorMessage == "Pedido deve possuir itens");
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Quantidade_For_Menor_Ou_Igual_Zero()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens =
            [
                new ItemPedidoDTO
                {
                    ProdutoNome = "Notebook",
                    Quantidade = 0,
                    PrecoUnitario = 3000
                }
            ]
        };

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeFalse();

        command.ValidationResult.Errors
            .Should()
            .Contain(x => x.PropertyName.Contains("Quantidade"));
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_PrecoUnitario_For_Menor_Ou_Igual_Zero()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens =
            [
                new ItemPedidoDTO
                {
                    ProdutoNome = "Notebook",
                    Quantidade = 1,
                    PrecoUnitario = 0
                }
            ]
        };

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeFalse();

        command.ValidationResult.Errors
            .Should()
            .Contain(x => x.PropertyName.Contains("PrecoUnitario"));
    }

    [Fact]
    public void Deve_Passar_Validacao_Quando_Command_For_Valido()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens =
            [
                new ItemPedidoDTO
                {
                    ProdutoNome = "Notebook",
                    Quantidade = 1,
                    PrecoUnitario = 3000
                }
            ]
        };

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeTrue();

        command.ValidationResult.Errors
            .Should()
            .BeEmpty();
    }
}