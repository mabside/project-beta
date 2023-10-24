using Review.Domain.DTOs.Spaces;

namespace Review.Domain.Entities.Spaces;

public partial class Space
{
    public static implicit operator SpaceInformation(Space space)
    {
        return new SpaceInformation
        {
            SpaceId = space.Id,
            BusinessId = space.BusinessId,
            Name = space.Name,
            Description = space.Description,
            ItemCounts = space.Items.Count()
        };
    }
}
