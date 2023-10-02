using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Businesses;

public class BusinessCategory : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ICollection<Business> Businesses { get; set; } = new List<Business>();

    private BusinessCategory() { }

    private BusinessCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<BusinessCategory> Create(
        string name, string description)
    {
        var result = Result<BusinessCategory>.Create(
            new BusinessCategory(
                name: name,
                description: description))
            .Validate(RequiredField.Create(name))
            .Validate(RequiredField.Create(description));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
