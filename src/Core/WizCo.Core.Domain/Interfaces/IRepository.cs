namespace WizCo.Core.Domain.Interfaces;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; }
    
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void Update(T entity);

    void Remove(T entity);
}