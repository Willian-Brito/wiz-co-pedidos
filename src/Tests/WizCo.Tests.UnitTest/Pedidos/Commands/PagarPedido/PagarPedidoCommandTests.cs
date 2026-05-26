using FluentAssertions;
using WizCo.Core.Application.UseCases.Pedidos.Commands.PagarPedido;

namespace WizCo.Tests.UnitTest.Pedidos.Commands.PagarPedido;

public class PagarPedidoCommandTests
{
    [Fact]
    public void Deve_Validar_Command_Com_Id_Valido()
    {
        // Arrange
        var command = new PagarPedidoCommand(Guid.NewGuid());

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeTrue();
        command.ValidationResult.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Deve_Retornar_Erro_Quando_Id_For_Vazio()
    {
        // Arrange
        var command = new PagarPedidoCommand(Guid.Empty);

        // Act
        var result = command.Validar();

        // Assert
        result.Should().BeFalse();

        command.ValidationResult.Errors
            .Should()
            .Contain(x => x.ErrorMessage == "Id do pedido é obrigatório");
    }
}