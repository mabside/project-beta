using Byhands.Models.Bases;
using MediatR;

namespace Byhands.CQRS.Interfaces;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, Result<TResult>> where TCommand : ICommand<TResult>
{
}
