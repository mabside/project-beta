using MediatR;
using Review.DataAccess;
using Review.Domain.DTOs.Spaces;
using Review.Domain.Entities.Spaces;
using Review.Models.Bases;

namespace Review.Application.Usecases.Spaces.CreateSpaces;

public class CreateSpaceCommandHandler : IRequestHandler<CreateSpaceCommand, Result<NewSpace>>
{
    private readonly IUnitOfWork uow;

    public CreateSpaceCommandHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<Result<NewSpace>> Handle(
        CreateSpaceCommand request,
        CancellationToken cancellationToken)
    {
        var result = Space.Create(
            name: request.Name,
            description: request.Description,
            businessId: request.BusinessId);

        if (result.HasError)
            return result.Error;

        var newSpace = result.Value;

        this.uow.SpaceRepository().Add(newSpace);

        await this.uow.CommitAsync(cancellationToken);

        return new NewSpace(newSpace.Id);
    }
}
