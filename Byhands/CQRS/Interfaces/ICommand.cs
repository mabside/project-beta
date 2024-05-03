using Byhands.Models.Bases;
using MediatR;

namespace Byhands.CQRS.Interfaces;

public interface ICommand<TResult> : IRequest<Result<TResult>>, IBaseRequest
{
}
