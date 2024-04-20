using Byhands.Entities.Validators;
using Byhands.Extensions;
using Byhands.Models.Bases;

namespace Byhands.Domain.Entities.Customers;

public class Customer : BaseEntity<Guid>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; private set; } = null!;
    public string BVN { get; set; }
    public string NIN { get; set; }

    private Customer() { }

    private Customer(
        string firstname,
        string lastname,
        string email,
        string phoneNumber)
    {
        Firstname = firstname;
        Lastname = lastname;
        Email = email;
        PhoneNumber = phoneNumber;
    }

    public static Result<Customer> Create(
        string firstname,
        string lastname,
        string email,
        string phoneNumber)
    {
        var result = Result<Customer>.Create(
            new Customer(
                firstname: firstname,
                lastname: lastname,
                email: email,
                phoneNumber: phoneNumber))
            .Validate(RequiredField.Create(firstname))
            .Validate(RequiredField.Create(lastname))
            .Validate(RequiredField.Create(email))
            .Validate(RequiredField.Create(phoneNumber));

        if (result.HasError)
            return result.Error;

        return result;
    }
}
