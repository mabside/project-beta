using Byhands.Entities.Validators;
using Byhands.Extensions;
using Byhands.Models.Bases;

namespace Byhands.Domain.Entities.Customers;

public class Customer : BaseEntity<Guid>
{
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; } = null!;

    private Customer() { }

    private Customer(string email, string phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static Result<Customer> Create(
        string email, string phoneNumber)
    {
        var result = Result<Customer>.Create(
            new Customer(
                email: email,
                phoneNumber: phoneNumber))
            .Validate(RequiredField.Create(email))
            .Validate(RequiredField.Create(phoneNumber));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
