using System.Text.Json.Serialization;
using FluentValidation.Results;
using MediatR;

namespace WizCo.Core.Application.Communication;

public abstract class Command<TResponse> : IRequest<TResponse>
{
    public DateTime Timestamp { get; }
    
    [JsonIgnore]
    public ValidationResult ValidationResult { get; set; }

    protected Command()
    {
        Timestamp = DateTime.Now;
        ValidationResult = new ValidationResult();
    }

    public abstract bool Validar();
}
