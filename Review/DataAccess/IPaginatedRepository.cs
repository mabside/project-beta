using System.Linq.Expressions;
using Review.Entities.QueryObjects;

namespace Review.DataAccess;

public interface IPaginatedRepository<TEntity, TQueryFilter>
    where TEntity : class
    where TQueryFilter : class
{
    Task<PaginatedResult<TEntity>> GetAllAsync(
        PaginatedQuery<TQueryFilter> queryFilter,
        Expression<Func<TEntity, bool>>? predicate = null,
        params string[] includes);

    Task<PaginatedResult<TResult>> GetAllAsync<TResult>(
        PaginatedQuery<TQueryFilter> queryFilter,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        params string[] includes)
        where TResult : class;
}
