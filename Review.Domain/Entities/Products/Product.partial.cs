using System.Collections.Immutable;
using Byhands.Domain.DTOs.Feedbacks;
using Byhands.Domain.DTOs.Products;

namespace Byhands.Domain.Entities.Products;

public partial class Product
{
    public static implicit operator ProductInformation(Product item)
    {
        return new ProductInformation
        (Id: item.Id,
         Description: item.Description,
         ImageUrl: item.ImageUrl,
         CategoryName: item.ProductCategory.Name,
         BusinessName: item.Business.Name,
         Feedbacks: item.Feedbacks.Select(f => (FeedbackInformation)f).ToImmutableList());
    }
}
