using Byhands.Application.Usecases.Customers.SigninCustomer;

namespace Byhands.API.Models.Auth.Requests;

public class SigninRequest
{
    public Guid UniqueRequestId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public static implicit operator SigninCustomerCommand(SigninRequest request)
    {
        return new SigninCustomerCommand(
            CommandId: request.UniqueRequestId,
            UserName: request.Email,
            Password: request.Password);
    }
}
