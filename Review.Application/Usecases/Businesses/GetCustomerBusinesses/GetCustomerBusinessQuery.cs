using MediatR;
using Byhands.Domain.DTOs.Businesses;
using Byhands.Models.Bases;

namespace Byhands.Application.Usecases.Businesses.GetCustomerBusinesses;

public record GetCustomerBusinessQuery(Guid CustomerId)
    : IRequest<Result<IReadOnlyCollection<BusinessInformation>>>;
