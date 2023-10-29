using FastEndpoints;
using Review.Application.Usecases.Items.CreateProduct;
using Review.Domain.DTOs.Items;
using Review.Models.Bases;

namespace Review.API.Endpoints.Items.CreateItem;

internal sealed class Mapper : Mapper<Request, Result<NewItem>, object>
{
    public static CreateItemCommand AsCommand(Request request)
    {
        return new CreateItemCommand(
            UniqueRequestId: request.UniqueRequestId,
            ItemCategoryId: request.ItemCategoryId,
            BusinessId: request.BusinessId,
            SpaceId: request.SpaceId,
            Name: request.Name,
            Description: request.Description,
            Image: request.File);
    }
}