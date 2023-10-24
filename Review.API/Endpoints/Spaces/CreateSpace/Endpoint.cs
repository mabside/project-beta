using FastEndpoints;
using MediatR;
using Review.Domain.DTOs.Businesses;
using Review.Domain.DTOs.Spaces;
using Review.Models.Bases;

namespace Review.API.Endpoints.Spaces.CreateSpace;

internal sealed class Endpoint : Endpoint<Request, Result<NewSpace>, Mapper>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/spaces");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = Mapper.AsCommand(req);
        var result = await this.mediator.Send(command, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<NewSpace> result, CancellationToken c)
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
