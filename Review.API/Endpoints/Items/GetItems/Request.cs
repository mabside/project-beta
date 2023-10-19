using Review.Application.Filters;
using Review.Application.Usecases.Items.GetItems;
using Review.Entities.QueryObjects;

namespace Review.API.Endpoints.Items.GetItems;

public class Request : PaginatedQuery<ItemsFilter>
{
    public Guid SpaceId { get; set; }
    public Guid BusinessId { get; set; }

    public static implicit operator GetItemsQuery(Request request)
    {
        return new GetItemsQuery(
            SpaceId: request.SpaceId,
            BusinessId: request.BusinessId,
            new PaginatedQuery<ItemsFilter>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                SortColumn = request.SortColumn,
                Order = request.Order,
                SearchFilter = request.SearchFilter,
            }
        );
    }
}
