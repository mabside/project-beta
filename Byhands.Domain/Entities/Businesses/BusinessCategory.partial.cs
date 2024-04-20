using Byhands.Domain.DTOs.Businesses;

namespace Byhands.Domain.Entities.Businesses;

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
