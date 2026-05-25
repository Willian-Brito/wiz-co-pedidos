namespace WizCo.Core.Domain.Interfaces;

public interface IRepository<T>
{
    Task AddAsync(T entity, CancellationToken cancellationToken = default);

    void Update(T entity);

    void Remove(T entity);
}