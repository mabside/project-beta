using Byhands.Application.Usecases.Customers.SigninCustomer;
using Byhands.Models.Bases;
using FastEndpoints;

namespace Byhands.API.Endpoints.Customers.Signin;

internal sealed class Mapper : Mapper<Request, Result<SigninCustomerCommandResponse>, object>
{
    public static SigninCustomerCommand AsCommand(Request request)
    {
        return new SigninCustomerCommand(
            CommandId: request.UniqueRequestId,
            UserName: request.Email,
            Password: request.Password);
    }
}
