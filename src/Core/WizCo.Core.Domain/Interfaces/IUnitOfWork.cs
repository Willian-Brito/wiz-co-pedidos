namespace WizCo.Core.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<bool> Commit();
}