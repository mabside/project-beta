using MediatR;
using Review.DataAccess;
using Review.Domain.DTOs.Items;
using Review.Domain.Entities.Items;
using Review.Models.Bases;

namespace Review.Application.Usecases.Items.CreateProduct;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Result<NewItem>>
{
    private readonly IUnitOfWork uow;

    public CreateItemCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public Task<Result<NewItem>> Handle(CreateItemCommand command, CancellationToken cancellationToken)
    {
        // create item
        var newItemResult = Item.Create(
            name: command.Name,
            )

        // create link
    }
}
