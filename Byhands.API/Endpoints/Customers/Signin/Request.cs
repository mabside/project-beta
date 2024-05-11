namespace Byhands.API.Endpoints.Customers.Signin;

public class Request
{
    public Guid UniqueRequestId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
