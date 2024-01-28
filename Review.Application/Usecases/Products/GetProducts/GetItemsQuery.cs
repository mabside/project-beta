using MediatR;
using Byhands.Application.Filters;
using Byhands.Domain.DTOs.Products;
using Byhands.Entities.QueryObjects;
using Byhands.Models.Bases;

namespace Byhands.Application.Usecases.Products.GetProducts;

public record GetProductsQuery(Guid SpaceId, Guid BusinessId, PaginatedQuery<ProductsFilter> Query)
    : IRequest<Result<PaginatedResult<ProductInformation>>>
{ }