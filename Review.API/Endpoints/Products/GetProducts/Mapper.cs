using FastEndpoints;
using Byhands.Application.Usecases.Products.GetProducts;
using Byhands.Domain.DTOs.Products;
using Byhands.Entities.QueryObjects;
using Byhands.Models.Bases;

namespace Byhands.API.Endpoints.Products.GetProducts;

internal sealed class Mapper : Mapper<Request, Result<PaginatedResult<ProductInformation>>, object>
{
    public static GetProductsQuery AsQuery(Request request)
    {
        return request;
    }
}
