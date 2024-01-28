using Byhands.Domain.DTOs.Businesses;

namespace Byhands.Domain.Entities.Businesses;

public partial class Business
{
    public static implicit operator BusinessInformation(Business business)
    {
        return new BusinessInformation()
        {
            Name = business.Name,
            Description = business.Description,
            Email = business.Email,
            LogoUrl = business.LogoUrl,
            Location = business.Location,
            BannerUrl = business.BannerUrl,
            WebsiteUrl = business.WebsiteUrl,
            SocialHandles = business.SocialHandles,
            BusinessCategoryId = business.BusinessCategoryId,
            CustomerId = business.CustomerId,
            Category = new CategoryInformation
            {
                Name = business.Category.Name,
                Description = business.Category.Description,
            }
        };
    }
}
