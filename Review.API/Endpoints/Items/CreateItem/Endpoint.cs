using FastEndpoints;
using MediatR;
using Review.Application.Interfaces.Providers;
using Review.Domain.DTOs.Items;
using Review.Domain.DTOs.Spaces;
using Review.Models.Bases;


namespace Review.API.Endpoints.Items.CreateItem;

public class Endpoint : Endpoint<Request, Result<NewItem>>
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
        Post("api/items");
        AllowFileUploads();
        AllowAnonymous();
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var command = Mapper.AsCommand(req);
        var result = await this.mediator.Send(command, ct);

        await SendResultAsync(result, ct);
    }

    private async Task SendResultAsync(Result<NewItem> result, CancellationToken c)
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
