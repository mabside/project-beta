using MediatR;
using Review.DataAccess;
using Review.Domain.DTOs.Spaces;
using Review.Entities.Errors;
using Review.Models.Bases;

namespace Review.Application.Usecases.Spaces.GetBusinessSpaces;

internal sealed class GetBusinessSpaceQueryHandler 
    : IRequestHandler<GetBusinessSpaceQuery, Result<IReadOnlyCollection<SpaceInformation>>>
{
    private readonly IUnitOfWork uow;

    public GetBusinessSpaceQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<Result<IReadOnlyCollection<SpaceInformation>>> Handle(
        GetBusinessSpaceQuery query, 
        CancellationToken cancellationToken)
    {
        var spaces = await this.uow.SpaceRepository().FindAsync(
            space => space.BusinessId == query.BusinessId, 
            asNoTracking: true);

        if (spaces == null)
            return new NullError("unable to find any spaces");

        var businessSpaces = spaces.Select(s => (SpaceInformation)s).ToList().AsReadOnly();

        return businessSpaces;
    }
}
