using Byhands.Application.Usecases.Customers.SigninCustomer;
using Byhands.Models.Bases;
using FastEndpoints;
using MediatR;

namespace Byhands.API.Endpoints.Customers.Signin;

public class Endpoint : Endpoint<Request, Result<SigninCustomerCommandResponse>>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/Customers/signin");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = Mapper.AsCommand(req);
        var result = await mediator.Send(command, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<SigninCustomerCommandResponse> result, CancellationToken c)
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
