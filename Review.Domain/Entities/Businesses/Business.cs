using System.Text.Json.Serialization;
using Review.Domain.Entities.Customers;
using Review.Domain.Entities.Feedbacks;
using Review.Domain.Entities.Spaces;
using Review.Entities.Validators;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.Domain.Entities.Businesses;

public partial class Business : BaseEntity<Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Email { get; private set; }
    public string? LogoUrl { get; private set; }
    public string? BannerUrl { get; private set; }
    public string? WebsiteUrl { get; private set; }
    public Location Location { get; private set; }
    public ICollection<SocialHandle> SocialHandles { get; private set; }
        = new List<SocialHandle>();

    public Guid BusinessCategoryId { get; private set; }
    public Guid CustomerId { get; private set; }

    public virtual BusinessCategory Category { get; private set; }
    public virtual Customer Customer { get; private set; }

    public virtual IEnumerable<Feedback> Reviews { get; set; }
     = new List<Feedback>();
    public virtual IEnumerable<Space> Spaces { get; set; }
     = new List<Space>();

    [JsonIgnore]
    public string FullLocation
    {
        get
        {
            var businessLocation = Location;

            if (businessLocation == null)
                return string.Empty;

            return string.Join(
                ", ",
                new[]
                {
                    businessLocation.Number,
                    businessLocation.Street,
                    businessLocation.City,
                    businessLocation.Country
                }
                .Where(s => !string.IsNullOrWhiteSpace(s)));
        }
    }

    private Business() { }

    private Business(
        string name,
        string description,
        string email,
        string? logoUrl,
        string? bannerUrl,
        string? websiteUrl,
        Guid businessCategoryId,
        BusinessCategory category,
        Guid customerId,
        Customer customer,
        Location location,
        ICollection<SocialHandle> socialHandles)
    {
        Name = name;
        Description = description;
        Email = email;
        LogoUrl = logoUrl;
        BannerUrl = bannerUrl;
        WebsiteUrl = websiteUrl;
        BusinessCategoryId = businessCategoryId;
        Category = category;
        CustomerId = customerId;
        Customer = customer;
        Location = location;
        SocialHandles = socialHandles;
    }

    public static Result<Business> Create(
        string name,
        string description,
        string email,
        string? logoUrl,
        string? bannerUrl,
        string? websiteUrl,
        Guid businessCategoryId,
        BusinessCategory category,
        Guid customerId,
        Customer customer,
        Location location,
        ICollection<SocialHandle>? socialHandles)
    {
        var result = Result<Business>.Create(
            new Business(
                name: name,
                description: description,
                email: email,
                logoUrl: logoUrl,
                bannerUrl: bannerUrl,
                websiteUrl: websiteUrl,
                category: category,
                businessCategoryId: businessCategoryId,
                customerId: customerId,
                customer: customer,
                location: location,
                socialHandles: socialHandles != null ? socialHandles : new List<SocialHandle>()))
            .Validate(RequiredField.Create(name))
            .Validate(NotNull.Create(businessCategoryId))
            .Validate(NotNull.Create(location));

        if (result.HasError)
            return result.Error;

        return result.Value;
    }
}
