using FastEndpoints;
using MediatR;
using Byhands.Domain.DTOs.Businesses;
using Byhands.Models.Bases;

namespace Byhands.API.Endpoints.Businesses.CreateBusinesses;

internal sealed class Endpoint : Endpoint<Request, Result<NewBusiness>, Mapper>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/businesses");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = Mapper.AsCommand(req);
        var result = await this.mediator.Send(query, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<NewBusiness> result, CancellationToken c)
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
