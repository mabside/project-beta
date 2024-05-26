using Byhands.Application.Usecases.Customers.SignupCustomer;

namespace Byhands.API.Models.Auth.Requests;

public class SignupRequest
{
    public Guid UniqueRequestId { get; set; }
    public string Firstname { get; set; } = string.Empty;
    public string Lastname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public static implicit operator SignupCustomerCommand(SignupRequest request)
    {
        return new SignupCustomerCommand(
            CommandId: request.UniqueRequestId,
            Firstname: request.Firstname,
            Lastname: request.Lastname,
            Email: request.Email,
            PhoneNumber: request.PhoneNumber,
            Password: request.Password);
    }
}