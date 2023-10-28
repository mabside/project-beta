using FastEndpoints;

namespace Review.API.Endpoints.Items.CreateItem;

public class Request
{
    public Guid UniqueRequestId { get; set; }
    public Guid ItemCategoryId { get; set; }
    public int SpaceId { get; set; }

    //[FromClaim("businessId")]
    public Guid BusinessId { get; set; }

}
