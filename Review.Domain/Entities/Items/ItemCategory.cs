using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Items;

public class ItemCategory : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public ICollection<Item> Items { get; private set; } = new List<Item>();

    private ItemCategory() { }

    private ItemCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static Result<ItemCategory> Create(
        string name, string description)
    {
        var result = Result<ItemCategory>.Create(
            new ItemCategory(
                name: name,
                description: description))
            .Validate(RequiredField.Create(name))
            .Validate(RequiredField.Create(description));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
