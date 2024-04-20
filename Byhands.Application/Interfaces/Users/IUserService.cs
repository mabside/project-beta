using Byhands.Models.Bases;

namespace Byhands.Application.Interfaces.Users;

public interface IUserService
{
    Task<Result> CreateIdentityAsync(
        Guid customerId,
        string userName,
        string password,
        Guid commandId,
        CancellationToken cancellationToken);
}
