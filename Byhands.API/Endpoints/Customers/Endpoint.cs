﻿using Byhands.Domain.DTOs.Customers;
using Byhands.Models.Bases;
using FastEndpoints;
using MediatR;

namespace Byhands.API.Endpoints.Customers;

public class Endpoint : Endpoint<Request, Result<NewCustomer>>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Post("api/Customers/signup");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = Mapper.AsCommand(req);
        var result = await this.mediator.Send(command, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<NewCustomer> result, CancellationToken c)
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
