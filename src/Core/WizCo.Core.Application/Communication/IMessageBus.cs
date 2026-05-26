namespace WizCo.Core.Application.Communication;

public interface IMessageBus
{
    Task<TResponse> SendCommand<TResponse>(Command<TResponse> command);
}