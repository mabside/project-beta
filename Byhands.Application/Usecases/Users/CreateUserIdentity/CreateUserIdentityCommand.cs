using Byhands.CQRS;
using Byhands.Models.Bases;
using MediatR;

namespace Byhands.Application.Usecases.Users.CreateUserIdentity;

public record CreateUserIdentityCommand(
    Guid UserId,
    Guid CommandId,
    string UserName,
    string Password
) : CommandBase<CreateUserIdentityCommandResponse>(CommandId);

public record CreateUserIdentityCommandResponse(bool Completed);