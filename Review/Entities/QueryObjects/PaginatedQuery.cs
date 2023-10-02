namespace Review.Entities.QueryObjects;

public class PaginatedQuery<T>
{
    public int? PageIndex { get; set; }
    public int? PageSize { get; set; }
    public string? SortColumn { get; set; }
    public QueryOrder? Order { get; set; } = QueryOrder.DESC;
    public T? SearchFilter { get; set; }
}
