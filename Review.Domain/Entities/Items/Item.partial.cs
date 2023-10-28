using System.Collections.Immutable;
using Review.Domain.DTOs.Feedbacks;
using Review.Domain.DTOs.Items;

namespace Review.Domain.Entities.Items;

public partial class Item
{
    public static implicit operator ItemInformation(Item item)
    {
        return new ItemInformation
        (Id: item.Id,
         Description: item.Description,
         ImageUrl: item.ImageUrl,
         CategoryName: item.ItemCategory.Name,
         SpaceName: item.Space.Name,
         Feedbacks: item.Feedbacks.Select(f => (FeedbackInformation)f).ToImmutableList());
    }
}
