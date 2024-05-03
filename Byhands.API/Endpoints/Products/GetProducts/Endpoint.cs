using Byhands.Domain.DTOs.Products;
using Byhands.Entities.QueryObjects;
using Byhands.Models.Bases;
using FastEndpoints;
using MediatR;

namespace Byhands.API.Endpoints.Products.GetProducts;

internal sealed class Endpoint : Endpoint<Request, Result<PaginatedResult<ProductInformation>>, Mapper>
{
    private readonly IMediator mediator;

    public Endpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public override void Configure()
    {
        Put("api/Products/{businessId}/{spaceId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var query = Mapper.AsQuery(req);
        var result = await this.mediator.Send(query, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<PaginatedResult<ProductInformation>> result, CancellationToken c)
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
