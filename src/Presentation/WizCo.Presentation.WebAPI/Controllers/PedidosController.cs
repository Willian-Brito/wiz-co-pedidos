using Microsoft.AspNetCore.Mvc;
using WizCo.Core.Application.Communication;
using WizCo.Core.Application.Results;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CancelarPedido;
using WizCo.Core.Application.UseCases.Pedidos.Commands.CriarPedido;
using WizCo.Core.Application.UseCases.Pedidos.Commands.PagarPedido;
using WizCo.Core.Application.UseCases.Pedidos.DTOs;
using WizCo.Core.Application.UseCases.Pedidos.Queries;
using WizCo.Core.Domain.Common;
using WizCo.Core.Domain.Enums;

namespace WizCo.Presentation.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IMessageBus _messageBus;
    private readonly IPedidoQueries _pedidoQueries;

    public PedidosController(IMessageBus messageBus, IPedidoQueries pedidoQueries)
    {
        _messageBus = messageBus;
        _pedidoQueries = pedidoQueries;
    }

    /// <summary>
    /// Cria um novo pedido
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Result<PedidoDTO>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] CriarPedidoCommand command)
    {
        if (!command.Validar())
            return BadRequest(command.ValidationResult.Errors);

        var result = await _messageBus.SendCommand(command);

        if (!result.Success)
            return BadRequest(result);

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = result.Data!.Id },
            result
        );
    }
    
    /// <summary>
    /// Cancela um pedido
    /// </summary>
    [HttpPut("{id:guid}/cancelar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cancelar(Guid id)
    {
        var command = new CancelarPedidoCommand(id);
        if (!command.Validar())
            return BadRequest(command.ValidationResult.Errors);

        var result = await _messageBus.SendCommand(command);

        if (!result.Success)
        {
            if (result.Errors.FirstOrDefault() == "Pedido não encontrado")
                return NotFound(result);

            return BadRequest(result);
        }

        return NoContent();
    }
    
    
    // <summary>
    /// Realiza pagamento do pedido
    /// </summary>
    [HttpPut("{id:guid}/pagar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Pagar(Guid id)
    {
        var command = new PagarPedidoCommand(id);

        if (!command.Validar())
            return BadRequest(command.ValidationResult.Errors);

        var result = await _messageBus.SendCommand(command);

        if (!result.Success)
        {
            if (result.Errors.FirstOrDefault() == "Pedido não encontrado")
                return NotFound(result);

            return BadRequest(result);
        }

        return NoContent();
    }

    /// <summary>
    /// Obtém pedido por Id
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(PedidoDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var result = await _pedidoQueries.GetByIdAsync(id);
        
        if (!result.Success)
            return NotFound(result);
        
        return Ok(result);
    }

    /// <summary>
    /// Lista pedidos paginados
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<PedidoDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObterTodos(
        [FromQuery] StatusPedido? status,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var pedidos = await _pedidoQueries.GetByStatusPaged(status, page, pageSize);
        return Ok(pedidos);
    }
}