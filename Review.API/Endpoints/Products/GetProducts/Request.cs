using Byhands.Application.Filters;
using Byhands.Application.Usecases.Products.GetProducts;
using Byhands.Entities.QueryObjects;

namespace Byhands.API.Endpoints.Products.GetProducts;

public class Request : PaginatedQuery<ProductsFilter>
{
    public Guid SpaceId { get; set; }
    public Guid BusinessId { get; set; }

    public static implicit operator GetProductsQuery(Request request)
    {
        return new GetProductsQuery(
            SpaceId: request.SpaceId,
            BusinessId: request.BusinessId,
            new PaginatedQuery<ProductsFilter>
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
