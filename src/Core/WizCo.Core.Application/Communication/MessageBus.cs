using MediatR;

namespace WizCo.Core.Application.Communication;

public class MessageBus : IMessageBus
{
    private readonly IMediator _mediator;

    public MessageBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> SendCommand<TResponse>(Command<TResponse> command)
    {
        return await _mediator.Send(command);
    }
}