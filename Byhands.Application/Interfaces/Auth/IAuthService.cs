using Byhands.Application.Usecases.Customers.SigninCustomer;
using Byhands.Application.Usecases.Customers.SignupCustomer;
using Byhands.Models.Bases;

namespace Byhands.Contract.Interfaces.Auth;

public interface IAuthService
{
    Task<Result<Guid>> CreateNewCustomerAsync(SignupCustomerCommand request, CancellationToken cancellationToken);

    Task<Result<SigninCustomerResponse>> SignInCustomerAsync(SigninCustomerCommand request, CancellationToken cancellationToken);
}
