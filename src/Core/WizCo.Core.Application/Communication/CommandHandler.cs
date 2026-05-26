using FluentValidation.Results;
using WizCo.Core.Domain.Interfaces;

namespace WizCo.Core.Application.Communication;

public abstract class CommandHandler
{
    protected ValidationResult ValidationResult;

    protected CommandHandler()
    {
        ValidationResult = new ValidationResult();
    }

    protected void AddError(string message)
    {
        ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
    }

    protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
    {
        if (!await uow.Commit()) AddError("An error occurred while trying to persist data");

        return ValidationResult;
    }
}