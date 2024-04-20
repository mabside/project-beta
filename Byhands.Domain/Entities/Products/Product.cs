using Byhands.Domain.Entities.Businesses;
using Byhands.Domain.Entities.Feedbacks;
using Byhands.Entities.Validators;
using Byhands.Extensions;
using Byhands.Models.Bases;
using Byhands.Utilities;

namespace Byhands.Domain.Entities.Products;

public partial class Product : BaseEntity<Guid>
{
    public string Name { get; private set; } = default!;
    public string Description { get; private set; } = default!;
    public string ImageUrl { get; private set; }
    public string Slug { get; private set; }

    public Guid ProductCategoryId { get; private set; }
    public Guid BusinessId { get; private set; }

    public virtual Business Business { get; private set; } = default!;
    public virtual ProductCategory ProductCategory { get; private set; } = default!;

    public IEnumerable<Feedback> Feedbacks { get; private set; }
     = new List<Feedback>();

    public Product() { }

    public Product(
        string name,
        string description,
        string imageUrl,
        Guid productCategoryId,
        Guid businessId,
        IEnumerable<Feedback> feedbacks)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
        productCategoryId = productCategoryId;
        BusinessId = businessId;
        Feedbacks = new List<Feedback>();
    }

    public static Result<Product> Create(
        Guid productCategoryId,
        Guid businessId,
        string name,
        string description,
        string imageUrl)
    {
        var result = Result<Product>.Create(
            new Product(
                name: name,
                productCategoryId: productCategoryId,
                description: description,
                businessId: businessId,
                imageUrl: imageUrl,
                feedbacks: new List<Feedback>()))
            .Validate(RequiredField.Create(name))
            .Validate(RequiredField.Create(description))
            .Validate(RequiredField.Create(imageUrl));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }

    public Product EmbedSlug()
    {
        Slug = SlugUtil.Slugify(this.Name);

        return this;
    }
}
