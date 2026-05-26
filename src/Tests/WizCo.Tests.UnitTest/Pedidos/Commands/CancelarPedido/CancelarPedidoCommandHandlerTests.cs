using FluentAssertions;
using Moq;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CancelarPedido;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Enums;
using WizCo.Core.Domain.Interfaces;
using WizCo.Tests.UnitTest.Pedidos.Factories;

namespace WizCo.Tests.UnitTest.Pedidos.Commands.CancelarPedido;

public class CancelarPedidoCommandHandlerTests
{
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly CancelarPedidoCommandHandler _handler;

    public CancelarPedidoCommandHandlerTests()
    {
        _pedidoRepositoryMock = new Mock<IPedidoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new CancelarPedidoCommandHandler(
            _pedidoRepositoryMock.Object,
            _unitOfWorkMock.Object
        );
    }

    [Fact]
    public async Task Deve_Cancelar_Pedido_Com_Sucesso()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();
        var command = new CancelarPedidoCommand(pedido.Id);

        _pedidoRepositoryMock
            .Setup(x => x.GetByIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        pedido.Status.Should().Be(StatusPedido.Cancelado);

        _pedidoRepositoryMock.Verify(x => x.Update(pedido), Times.Once);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }

    [Fact]
    public async Task Deve_Retornar_Falha_Quando_Pedido_Nao_For_Encontrado()
    {
        // Arrange
        var command = new CancelarPedidoCommand(Guid.NewGuid());

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
    public async Task Deve_Lancar_Excecao_Quando_Tentar_Cancelar_Pedido_Pago()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();
        pedido.MarcarComoPago();
        var command = new CancelarPedidoCommand(pedido.Id);
        
        _pedidoRepositoryMock
            .Setup(x => x.GetByIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Pedido pago não pode ser cancelado");

        _pedidoRepositoryMock.Verify(x => x.Update(It.IsAny<Pedido>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Never);
    }

    [Fact]
    public async Task Deve_Chamar_Update_Ao_Cancelar_Pedido()
    {
        // Arrange
        var pedido = PedidoFactory.Criar();
        var command = new CancelarPedidoCommand(pedido.Id);

        _pedidoRepositoryMock
            .Setup(x => x.GetByIdAsync(pedido.Id))
            .ReturnsAsync(pedido);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _pedidoRepositoryMock.Verify(x => x.Update(It.IsAny<Pedido>()), Times.Once);
    }
}