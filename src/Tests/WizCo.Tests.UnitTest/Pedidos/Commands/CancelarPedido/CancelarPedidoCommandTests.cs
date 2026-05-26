using FluentAssertions;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CancelarPedido;

namespace WizCo.Tests.UnitTest.Pedidos.Commands.CancelarPedido;

public class CancelarPedidoCommandTests
{
    [Fact]
    public void Deve_Retornar_Erro_Quando_Id_For_Vazio()
    {
        // Arrange
        var command = new CancelarPedidoCommand(Guid.Empty);

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeFalse();

        command.ValidationResult.Errors
            .Should()
            .Contain(x => x.ErrorMessage == "Id do pedido é obrigatório");
    }

    [Fact]
    public void Deve_Passar_Validacao_Quando_Id_For_Valido()
    {
        // Arrange
        var command = new CancelarPedidoCommand(Guid.NewGuid());

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeTrue();

        command.ValidationResult.Errors
            .Should()
            .BeEmpty();
    }
}