using Review.Domain.Entities.Businesses;
using Review.Domain.Entities.Items;
using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Spaces;

public partial class Space : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    public Guid BusinessId { get; private set; }

    public virtual Business Business { get; private set; } = default!;
    public virtual IEnumerable<Item> Items { get; private set; }
     = new List<Item>();

    private Space() { }

    private Space(
        string name,
        string description,
        Guid businessId)
    {
        Name = name;
        Description = description;
        BusinessId = businessId;
    }

    public static Result<Space> Create(
        string name,
        string description,
        Guid businessId)
    {
        var result = Result<Space>.Create(
            new Space(
                name: name,
                description: description,
                businessId: businessId))
            .Validate(RequiredField.Create(name))
            .Validate(RequiredField.Create(description))
            .Validate(NotNull.Create(businessId));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}