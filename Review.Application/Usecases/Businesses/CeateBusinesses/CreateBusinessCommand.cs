using MediatR;
using Review.Domain.DTOs.Businesses;
using Review.Domain.Entities.Businesses;
using Review.Models.Bases;

namespace Review.Application.Usecases.Businesses.CeateBusinesses;

public record CreateBusinessCommand(
    string Name,
    string Description,
    string Email,
    string LogoUrl,
    string BannerUrl,
    string WebsiteUrl,
    string Number,
    string City,
    string State,
    string Street,
    string Country,
    string PostalCode,
    BusinessCategory Category,
    Guid BusinessCategoryId) : IRequest<Result<NewBusiness>>;