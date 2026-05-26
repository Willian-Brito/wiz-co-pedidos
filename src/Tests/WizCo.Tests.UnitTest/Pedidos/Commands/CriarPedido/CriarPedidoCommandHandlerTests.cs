using AutoMapper;
using FluentAssertions;
using Moq;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Domain.Entities;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Tests.UnitTest.Pedidos.Commands.CriarPedido;

public class CriarPedidoCommandHandlerTests
{
    private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly CriarPedidoCommandHandler _handler;

    public CriarPedidoCommandHandlerTests()
    {
        _pedidoRepositoryMock = new Mock<IPedidoRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _mapperMock = new Mock<IMapper>();

        _handler = new CriarPedidoCommandHandler(
            _pedidoRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Deve_Criar_Pedido_Com_Sucesso()
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
                    Quantidade = 2,
                    PrecoUnitario = 3500
                }
            ]
        };

        var pedidoDto = new PedidoDTO
        {
            ClienteNome = "Willian",
            ValorTotal = 7000
        };

        _mapperMock
            .Setup(x => x.Map<PedidoDTO>(It.IsAny<Pedido>()))
            .Returns(pedidoDto);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.ClienteNome.Should().Be("Willian");
        result.Data.ValorTotal.Should().Be(7000);

        _pedidoRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<Pedido>()),
            Times.Once
        );

        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }

    [Fact]
    public async Task Deve_Mapear_Corretamente_O_Pedido()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens =
            [
                new ItemPedidoDTO
                {
                    ProdutoNome = "Mouse",
                    Quantidade = 1,
                    PrecoUnitario = 150
                }
            ]
        };

        _mapperMock
            .Setup(x => x.Map<PedidoDTO>(
                It.IsAny<Pedido>()))
            .Returns(new PedidoDTO
            {
                ClienteNome = "Willian",
                ValorTotal = 150
            });

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Data.ValorTotal.Should().Be(150);
    }

    [Fact]
    public async Task Deve_Chamar_UnitOfWork_Apos_Criar_Pedido()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens =
            [
                new ItemPedidoDTO
                {
                    ProdutoNome = "Teclado",
                    Quantidade = 1,
                    PrecoUnitario = 500
                }
            ]
        };

        _mapperMock
            .Setup(x => x.Map<PedidoDTO>(It.IsAny<Pedido>()))
            .Returns(new PedidoDTO());

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Once);
    }
    
    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Quantidade_For_Menor_Ou_Igual_Zero()
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
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Quantidade deve ser maior que zero");

        _pedidoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Pedido>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Never);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Quando_Nao_Possuir_Itens()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian",
            Itens = []
        };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Pedido deve possuir ao menos um item");

        _pedidoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Pedido>()), Times.Never);
        _unitOfWorkMock.Verify(x => x.Commit(), Times.Never);
    }
}