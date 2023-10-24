using FastEndpoints;
using MediatR;
using Review.Application.Usecases.Businesses.GetCustomerBusinesses;
using Review.Domain.DTOs.Businesses;
using Review.Models.Bases;

namespace Review.API.Endpoints.Businesses.GetCustomerBusiness;

public class Endpoint : Endpoint<Request, Result<IReadOnlyCollection<BusinessInformation>>>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Get("api/businesses/customer/{customerId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = new GetCustomerBusinessQuery(req.CustomerId);
        var result = await this.mediator.Send(query, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<IReadOnlyCollection<BusinessInformation>> result, CancellationToken c)
    {
        if (result.HasError)
        {
            AddError(result.Error.Message);
            await SendErrorsAsync(cancellation: c);
        }
        else
        {
            await SendAsync(result, cancellation: c);
        }
    }
}
