using Byhands.Entities.Validators;
using Byhands.Extensions;
using Byhands.Models.Bases;

namespace Byhands.Domain.Entities.Products;

public class ProductCategory : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ICollection<Product> Items { get; private set; } = new List<Product>();

    private ProductCategory() { }

    private ProductCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<ProductCategory> Create(
        string name, string description)
    {
        var result = Result<ProductCategory>.Create(
            new ProductCategory(
                name: name,
                description: description))
            .Validate(RequiredField.Create(name))
            .Validate(RequiredField.Create(description));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
