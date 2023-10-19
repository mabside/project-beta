using MediatR;
using Review.Application.Filters;
using Review.Domain.DTOs.Items;
using Review.Entities.QueryObjects;
using Review.Models.Bases;

namespace Review.Application.Usecases.Items.GetItems;

public record GetItemsQuery(Guid SpaceId, Guid BusinessId, PaginatedQuery<ItemsFilter> Query)
    : IRequest<Result<PaginatedResult<ItemInformation>>>
{ }