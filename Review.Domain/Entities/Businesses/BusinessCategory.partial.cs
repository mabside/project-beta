using Review.Domain.DTOs.Businesses;

namespace Review.Domain.Entities.Businesses;

public partial class BusinessCategory
{
    public static implicit operator CategoryInformation(BusinessCategory category)
    {
        return new CategoryInformation()
        {
            Name = category.Name,
            Description = category.Description
        };
    }
}
