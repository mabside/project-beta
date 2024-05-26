using Byhands.Models.Bases;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore.Storage;

namespace Byhands.Contract;

public interface IUnitOfWork
{
    IDbContextTransaction Begin(ICapPublisher capPublisher, bool autoCommit = false);

    Task<Result> CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
}