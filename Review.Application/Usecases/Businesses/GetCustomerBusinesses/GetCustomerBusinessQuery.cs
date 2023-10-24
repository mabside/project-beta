using MediatR;
using Review.Domain.DTOs.Businesses;
using Review.Models.Bases;

namespace Review.Application.Usecases.Businesses.GetCustomerBusinesses;

public record GetCustomerBusinessQuery(Guid CustomerId)
    : IRequest<Result<IReadOnlyCollection<BusinessInformation>>>;
