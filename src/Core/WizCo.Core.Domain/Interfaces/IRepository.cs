namespace WizCo.Core.Domain.Interfaces;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
    
    Task AddAsync(T entity);

    void Update(T entity);

    void Remove(T entity);
}