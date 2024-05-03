using Byhands.CQRS.Interfaces;
using Byhands.Models.Bases;
using MediatR;

namespace Byhands.CQRS;

public abstract record CommandBase<TResult>(Guid Id) : ICommand<TResult>, IRequest<Result<TResult>>, IBaseRequest
{
    protected CommandBase(CommandBase<TResult> original)
    {
        Id = original.Id;
    }
}
