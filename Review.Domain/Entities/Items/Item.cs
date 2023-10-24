using Review.Domain.Entities.Feedbacks;
using Review.Domain.Entities.Links;
using Review.Domain.Entities.Spaces;
using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Items;

public partial class Item : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string ImageUrl { get; set; }

    public Guid LinkId { get; private set; }
    public Guid ItemCategoryId { get; private set; }
    public Guid SpaceId { get; private set; }

    public virtual Link Link { get; private set; } = default!;
    public virtual Space Space { get; private set; } = default!;
    public virtual ItemCategory ItemCategory { get; private set; } = default!;

    public IEnumerable<Feedback> Feedbacks { get; private set; }
     = new List<Feedback>();

    public Item() { }

    public Item(
        string name,
        Guid linkId,
        Guid itemCategoryId,
        Guid spaceId,
        IEnumerable<Feedback> feedbacks)
    {
        Name = name;
        LinkId = linkId;
        ItemCategoryId = itemCategoryId;
        SpaceId = spaceId;
        Feedbacks = new List<Feedback>();
    }

    public static Result<Item> Create(
        string name,

        Guid linkId,
        Guid itemCategoryId,
        Guid spaceId)
    {
        var result = Result<Item>.Create(
            new Item(
                name: name,
                linkId: linkId,
                itemCategoryId: itemCategoryId,
                spaceId: spaceId,
                feedbacks: new List<Feedback>()))
            .Validate(RequiredField.Create(name));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
