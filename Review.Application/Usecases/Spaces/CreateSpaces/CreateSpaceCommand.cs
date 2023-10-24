using MediatR;
using Review.Domain.DTOs.Spaces;
using Review.Models.Bases;

namespace Review.Application.Usecases.Spaces.CreateSpaces;

public record CreateSpaceCommand(
    Guid BusinessId,
    string Name,
    string Description)
    : IRequest<Result<NewSpace>>;