using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Byhands.Abstractions.Entities;
using Byhands.Domain;
using Byhands.Entities.QueryObjects;
using Byhands.Extensions;

namespace Byhands.DataAccess;

public abstract class PaginatedRepository<TDbContext, TEntity, TQueryFilter, TId> :
    EFContextRepositoryBase<TDbContext, TEntity, TId>,
    IPaginatedRepository<TEntity, TQueryFilter>,
    IReadOnlyRepository<TEntity, TId>,
    IWriteOnlyRepository<TEntity, TId>
    where TId : notnull
    where TDbContext : DbContext
    where TQueryFilter : class
    where TEntity : class, IEntity<TId>
{
    protected PaginatedRepository(TDbContext context) : base(context)
    {
    }

    protected virtual IQueryable<TEntity> Filter(
        IQueryable<TEntity> query,
        TQueryFilter queryFilter)
        => query;

    public async Task<PaginatedResult<TEntity>> GetAllAsync(
        PaginatedQuery<TQueryFilter> paginatedQuery,
        Expression<Func<TEntity, bool>>? predicate = null,
        params string[] includes)
    {
        return await BuilQuery(paginatedQuery, predicate, includes).GetPagedResultAsync(paginatedQuery);
    }

    public async Task<PaginatedResult<TResult>> GetAllAsync<TResult>(
        PaginatedQuery<TQueryFilter> paginatedQuery,
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? predicate = null,
        params string[] includes)
        where TResult : class
    {
        return await BuilQuery(paginatedQuery, predicate, includes)
                        .Select(selector)
                        .GetPagedResultAsync(paginatedQuery);
    }

    private IQueryable<TEntity> BuilQuery(PaginatedQuery<TQueryFilter> paginatedQuery, Expression<Func<TEntity, bool>>? predicate, string[] includes)
    {
        IQueryable<TEntity> query = _dbContext.Set<TEntity>();

        if (paginatedQuery.SearchFilter is not null)
        {
            query = Filter(query, paginatedQuery.SearchFilter);
        }

        if (!PaginatedRepositoryHelper.IsValidSortColumn<TEntity>(paginatedQuery.SortColumn))
        {
            paginatedQuery.SortColumn = nameof(BaseEntity<int>.CreatedOn);
        }

        if (predicate != null)
            query = query.Where(predicate);

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        if (paginatedQuery.Order == QueryOrder.DESC)
            query = query.OrderByColumnDescending(paginatedQuery.SortColumn!);
        else
            query = query.OrderByColumn(paginatedQuery.SortColumn!);

        return query.AsNoTracking();
    }
}
