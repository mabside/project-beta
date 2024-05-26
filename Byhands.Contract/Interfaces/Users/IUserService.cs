using Byhands.Models.Bases;

namespace Byhands.Contract.Interfaces.Users;

public interface IUserService
{
    Task<Result<bool>> CreateIdentityAsync(
        Guid customerId,
        string userName,
        string password,
        Guid commandId,
        CancellationToken cancellationToken);

    Task<Result<SigninCustomerResponse>> SignInAsync(
        string username,
        string password,
        Guid CommandId,
        CancellationToken cancellationToken);
}
