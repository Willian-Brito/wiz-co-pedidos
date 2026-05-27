using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;
using WizCo.Tests.IntegrationTest.Configurations;

namespace WizCo.Tests.IntegrationTest.Controllers;

public class PedidosControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public PedidosControllerTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Criar_DeveRetornar201_QuandoPedidoValido()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "Willian Brito",
            Itens =
            [
                new()
                {
                    ProdutoNome = "Notebook Dell",
                    Quantidade = 1,
                    PrecoUnitario = 5000
                }
            ]
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/pedidos", command);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Criar_DeveRetornar400_QuandoPedidoInvalido()
    {
        // Arrange
        var command = new CriarPedidoCommand
        {
            ClienteNome = "",
            Itens = []
        };

        // Act
        var response = await _client.PostAsJsonAsync("/api/pedidos", command);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task ObterPorId_DeveRetornar404_QuandoPedidoNaoExistir()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync($"/api/pedidos/{id}");

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task ObterTodos_DeveRetornar200()
    {
        // Act
        var response = await _client.GetAsync("/api/pedidos");

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Cancelar_DeveRetornar404_QuandoPedidoNaoExistir()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await _client.PutAsync($"/api/pedidos/{id}/cancelar", null);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Pagar_DeveRetornar404_QuandoPedidoNaoExistir()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await _client.PutAsync($"/api/pedidos/{id}/pagar", null);

        // Assert
        response.StatusCode
            .Should()
            .Be(HttpStatusCode.NotFound);
    }
}