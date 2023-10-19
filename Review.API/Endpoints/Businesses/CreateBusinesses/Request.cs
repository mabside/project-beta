using Review.Application.Usecases.Businesses.CeateBusinesses;

namespace Review.API.Endpoints.Businesses.CreateBusinesses;

public class Request
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty; 
    public string BannerUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string PostalCode { get; set; } = string.Empty;
    public Guid BusinessCategoryId { get; set; }

    public static implicit operator CreateBusinessCommand(Request request)
    {
        return new CreateBusinessCommand(
            Name: request.Name,
            Description: request.Description,
            Email: request.Email,
            LogoUrl: request.LogoUrl,
            BannerUrl: request.BannerUrl,
            WebsiteUrl: request.WebsiteUrl,
            Number: request.Number,
            City: request.City,
            State: request.State,
            Street: request.Street,
            Country: request.Country,
            PostalCode: request.PostalCode,
            BusinessCategoryId: request.BusinessCategoryId);
    }
}
