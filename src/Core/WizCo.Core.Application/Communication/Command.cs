using FluentValidation.Results;
using MediatR;

namespace WizCo.Core.Application.Communication;

public abstract class Command<TResponse> : IRequest<TResponse>
{
    public DateTime Timestamp { get; }
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.UtcNow;
        ValidationResult = new ValidationResult();
    }

    public abstract bool Validar();
}
