namespace Byhands.Entities.QueryObjects
{
    public record struct PaginatedResult<T>(
        int CurrentPage,
        int PageCount,
        int PageSize,
        int TotalRecords,
        IEnumerable<T> Result
    );
}
