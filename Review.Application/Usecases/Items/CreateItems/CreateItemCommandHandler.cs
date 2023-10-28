using MediatR;
using Review.DataAccess;
using Review.Domain.DTOs.Items;
using Review.Domain.Entities.Items;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Application.Usecases.Items.CreateProduct;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<NewItem>>
{
    private readonly IUnitOfWork uow;

    public CreateItemCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<Result<NewItem>> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        var itemCategory = await uow.ItemCategoryRepository().GetAsync(command.ItemCategoryId);

        if (itemCategory == null)
            return new NullError($"item category with id {command.ItemCategoryId} cannot be found");

        var space = await uow.SpaceRepository().FirstOrDefaultAsync(
            space => space.Id == command.SpaceId 
            && space.BusinessId == command.BusinessId);

        if (space == null)
            return new NullError($"space id {command.SpaceId} cannot be found for user");


        var result = Item.Create(
            name: command.Name,
            description: command.Description,
            itemCategoryId: command.ItemCategoryId,
            spaceId: command.SpaceId);

        if (result.HasError)
            return result.Error;

        var newItem = result.Value;

        newItem = newItem.EmbedSlug();

        uow.ItemRepository().Add(newItem);
        await uow.CommitAsync(cancellationToken);

        return new NewItem(newItem.Id);
    }
}
