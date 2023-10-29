using MediatR;
using Review.Domain.DTOs.Spaces;
using Review.Models.Bases;

namespace Review.Application.Usecases.Spaces.GetBusinessSpaces;

public record GetBusinessSpaceQuery(Guid BusinessId)
    : IRequest<Result<IReadOnlyCollection<SpaceInformation>>>;