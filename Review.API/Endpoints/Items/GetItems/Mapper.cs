using FastEndpoints;
using Review.Application.Usecases.Items.GetItems;
using Review.Domain.DTOs.Items;
using Review.Entities.QueryObjects;
using Review.Models.Bases;

namespace Review.API.Endpoints.Items.GetItems;

internal sealed class Mapper : Mapper<Request, Result<PaginatedResult<ItemInformation>>, object>
{
    public static GetItemsQuery AsQuery(Request request)
    {
        return request;
    }
}
