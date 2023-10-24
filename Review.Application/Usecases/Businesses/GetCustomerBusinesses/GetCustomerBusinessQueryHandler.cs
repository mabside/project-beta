using MediatR;
using Review.DataAccess;
using Review.Domain.DTOs.Businesses;
using Review.Domain.Entities.Businesses;
using Review.Models.Bases;

namespace Review.Application.Usecases.Businesses.GetCustomerBusinesses;

internal class GetCustomerBusinessQueryHandler : IRequestHandler<GetCustomerBusinessQuery, Result<IReadOnlyCollection<BusinessInformation>>>
{
    private readonly IUnitOfWork uow;

    public GetCustomerBusinessQueryHandler(IUnitOfWork uow)
    {
        this.uow = uow;
    }

    public async Task<Result<IReadOnlyCollection<BusinessInformation>>> Handle(
        GetCustomerBusinessQuery request, CancellationToken cancellationToken)
    {
        var businesses = await uow.BusinessRepository().FindAsync(
            business => business.CustomerId == request.CustomerId,
            asNoTracking: true,
            nameof(Business.Category));

        var customerBusinesses = businesses.Select(b => (BusinessInformation)b)
            .ToList().AsReadOnly();

        return customerBusinesses;
    }
}
