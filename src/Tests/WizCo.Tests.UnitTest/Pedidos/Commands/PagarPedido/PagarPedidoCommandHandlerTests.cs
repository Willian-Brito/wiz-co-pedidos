using FluentAssertions;
using Moq;
using WizCo.Core.Application.UseCases.Pedidos.Commands.PagarPedido;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Interfaces;
using WizCo.Tests.UnitTest.Pedidos.Factories;

namespace WizCo.Tests.UnitTest.Pedidos.Commands.PagarPedido;

public class PagarPedidoCommandHandlerTests
{
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly PagarPedidoCommandHandler _handler;

    public PagarPedidoCommandHandlerTests()
    {
        _pedidoRepositoryMock = new Mock<IPedidoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new PagarPedidoCommandHandler(
            _pedidoRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Deve_Pagar_Pedido_Com_Sucesso()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();
        var command = new PagarPedidoCommand(pedido.Id);

        _pedidoRepositoryMock
            .Setup(x => x.GetByIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        _unitOfWorkMock
            .Setup(x => x.Commit())
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        pedido.Status.Should().Be(StatusPedido.Pago);

        _pedidoRepositoryMock.Verify(x => x.Update(pedido), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Pedido_Nao_For_Encontrado()
    {
        // Arrange
        var command = new PagarPedidoCommand(Guid.NewGuid());

        _pedidoRepositoryMock
            .Setup(x => x.GetByIdAsync(command.Id))
            .ReturnsAsync((Pedido?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
        result.Errors.FirstOrDefault().Should().Be("Pedido não encontrado");

        _pedidoRepositoryMock.Verify(x => x.Update(It.IsAny<Pedido>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Never);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Pedido_Estiver_Cancelado()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();

        pedido.Cancelar();

        var command = new PagarPedidoCommand(pedido.Id);

        _pedidoRepositoryMock
            .Setup(x => x.GetByIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Pedido cancelado não pode ser pago");

        _pedidoRepositoryMock.Verify(x => x.Update(It.IsAny<Pedido>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Never);
    }
}