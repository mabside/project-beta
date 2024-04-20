using Byhands.Abstractions.Entities;

namespace Byhands.DataAccess;

public interface IWriteOnlyRepository<TEntity, TId> where TEntity : IEntity<TId> where TId : notnull
{
    void Add(TEntity entity);

    void Update(TEntity entity);

    void Delete(TEntity entity);

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteAsync(TEntity entity);
}
