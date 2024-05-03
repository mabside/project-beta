using Byhands.Application.Usecases.Customers.SignupCustomer;
using Byhands.Domain.DTOs.Customers;
using Byhands.Models.Bases;
using FastEndpoints;

namespace Byhands.API.Endpoints.Customers.Signup;

internal sealed class Mapper : Mapper<Request, Result<NewCustomer>, object>
{
    public static SignupCustomerCommand AsCommand(Request request)
    {
        return new SignupCustomerCommand(
            CommandId: request.UniqueRequestId,
            firstname: request.Firstname,
            lastname: request.Lastname,
            email: request.Email,
            phoneNumber: request.PhoneNumber,
            password: request.Password);
    }
}
