using FastEndpoints;
using MediatR;
using Byhands.Application.Interfaces.Providers;
using Byhands.Domain.DTOs.Products;
using Byhands.Models.Bases;


namespace Byhands.API.Endpoints.Products.CreateProduct;

public class Endpoint : Endpoint<Request, Result<NewProduct>>
{
    private readonly IMediator mediator;
    private readonly IUploadService uploadService;

    public Endpoint(IMediator mediator, IUploadService uploadService)
    {
        this.mediator = mediator;
        this.uploadService = uploadService;
    }

    public override void Configure()
    {
        Post("api/Products");
        AllowFileUploads();
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = Mapper.AsCommand(req);
        var result = await this.mediator.Send(command, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<NewProduct> result, CancellationToken c)
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
