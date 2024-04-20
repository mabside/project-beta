using Byhands.Entities.QueryObjects;
using Microsoft.EntityFrameworkCore;

namespace Byhands.Extensions;

public static class PaginationExtension
{
    /// <summary>
    /// For mocking sake, would be removed when concrete implementation begins
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TFilter"></typeparam>
    /// <param name="source"></param>
    /// <param name="query"></param>
    /// <returns></returns>
    public static PaginatedResult<T> GetPagedResult<T, TFilter>(this IEnumerable<T> source, PaginatedQuery<TFilter> query)
        where T : class
        where TFilter : class
    {
        var result = new PaginatedResult<T>
        {
            TotalRecords = source.Count()
        };

        if (query.PageIndex <= 0 || query.PageSize <= 0)
        {
            result.Result = source.ToList();
            return result;
        }

        var pageIndex = query.PageIndex <= 0 ? 1 : query.PageIndex ?? 1;
        var pageSize = query.PageSize ?? 20;

        result.CurrentPage = pageIndex;
        result.PageSize = pageSize;

        var pageCount = (double)result.TotalRecords / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (pageIndex - 1) * pageSize;
        result.Result = source.Skip(skip).Take(pageSize).ToList();

        // TODO: Implement filter functionality

        return result;
    }

    public static async Task<PaginatedResult<T>> GetPagedResultAsync<T, TFilter>(this IQueryable<T> source, PaginatedQuery<TFilter> query)
        where T : class
        where TFilter : class
    {
        var result = new PaginatedResult<T>
        {
            TotalRecords = await source.CountAsync()
        };

        if (query.PageIndex <= 0 || query.PageSize <= 0)
        {
            result.Result = await source.ToListAsync();
            return result;
        }

        var pageIndex = query.PageIndex <= 0 ? 1 : query.PageIndex ?? 1;
        var pageSize = query.PageSize ?? 20;

        result.PageSize = pageSize;
        result.CurrentPage = pageIndex;

        var pageCount = (double)result.TotalRecords / pageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (pageIndex - 1) * pageSize;
        result.Result = await source.Skip(skip).Take(pageSize).ToListAsync();

        return result;
    }

    public static async Task<PaginatedResult<T>> GetPagedResultAsync<T>(this IQueryable<T> source, int? pageIndex, int? pageSize)
        where T : class
    {
        var result = new PaginatedResult<T>
        {
            TotalRecords = await source.CountAsync()
        };

        if (pageIndex <= 0 || pageSize <= 0)
        {
            result.Result = await source.ToListAsync();
            return result;
        }

        var mPageIndex = pageIndex <= 0 ? 1 : pageIndex ?? 1;
        var mPageSize = pageSize ?? 20;

        result.CurrentPage = mPageIndex;
        result.PageSize = mPageSize;

        var pageCount = (double)result.TotalRecords / mPageSize;
        result.PageCount = (int)Math.Ceiling(pageCount);

        var skip = (mPageIndex - 1) * mPageSize;
        result.Result = source.Skip(skip).Take(mPageSize).ToList();

        return result;
    }
}
