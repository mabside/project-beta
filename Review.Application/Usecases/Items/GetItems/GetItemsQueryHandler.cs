using MediatR;
using Review.Application.Interfaces;
using Review.Domain.DTOs.Items;
using Review.Domain.Entities.Items;
using Review.Entities.Errors;
using Review.Entities.QueryObjects;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Application.Usecases.Items.GetItems;

internal class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, Result<PaginatedResult<ItemInformation>>>
{
    private readonly IItemRepository repository;

    public GetItemsQueryHandler(IItemRepository repository)
    {
        this.repository = repository;
    }

    public async Task<Result<PaginatedResult<ItemInformation>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        var itemList = await this.repository.GetAllAsync(
            item => item.SpaceId == request.SpaceId &&
            item.Space.BusinessId == request.BusinessId, 
            includes: new string[] { nameof(Item.Space), nameof(Item.ItemCategory) });

        if (itemList == null)
            return new NullError("Space is empty");

        var itemInfoList = itemList.Select(item => (ItemInformation)item).ToList();

        return itemInfoList.GetPagedResult(request.Query);
    }
}
