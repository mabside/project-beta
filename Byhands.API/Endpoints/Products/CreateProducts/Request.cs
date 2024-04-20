namespace Byhands.API.Endpoints.Products.CreateProduct;

public class Request
{
    public Guid UniqueRequestId { get; set; }
    public Guid ProductCategoryId { get; set; }
    public Guid SpaceId { get; set; }

    //[FromClaim("businessId")]
    public Guid BusinessId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public IFormFile File { get; set; }
}
