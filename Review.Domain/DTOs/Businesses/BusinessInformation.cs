using Review.Domain.Entities.Businesses;

namespace Review.Domain.DTOs.Businesses;

public class BusinessInformation
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string? LogoUrl { get; set; }
    public string? BannerUrl { get; set; }
    public string? WebsiteUrl { get; set; }
    public Location Location { get; set; }
    public ICollection<SocialHandle> SocialHandles { get; set; }
        = new List<SocialHandle>();

    public Guid BusinessCategoryId { get; set; }
    public Guid CustomerId { get; set; }

    public CategoryInformation Category { get; set; }
        = new();
}
