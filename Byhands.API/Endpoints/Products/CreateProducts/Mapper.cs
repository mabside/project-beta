using Byhands.Application.Usecases.Products.CreateProduct;
using Byhands.Domain.DTOs.Products;
using Byhands.Models.Bases;
using FastEndpoints;

namespace Byhands.API.Endpoints.Products.CreateProduct;

internal sealed class Mapper : Mapper<Request, Result<NewProduct>, object>
{
    public static CreateProductCommand AsCommand(Request request)
    {
        return new CreateProductCommand(
            UniqueRequestId: request.UniqueRequestId,
            ProductCategoryId: request.ProductCategoryId,
            BusinessId: request.BusinessId,
            Name: request.Name,
            Description: request.Description,
            Image: request.File);
    }
}