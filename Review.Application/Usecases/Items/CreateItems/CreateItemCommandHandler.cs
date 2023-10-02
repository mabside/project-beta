using MediatR;

namespace Review.Application.Usecases.Items.CreateProduct;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand>
{
    public Task Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
