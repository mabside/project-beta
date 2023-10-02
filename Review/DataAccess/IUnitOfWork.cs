using Microsoft.EntityFrameworkCore.Storage;
using Review.Models.Bases;

namespace Review.DataAccess;

public interface IUnitOfWork : IAsyncDisposable
{
    //IDbContextTransaction Begin(ICapPublisher capPublisher, bool autoCommit = false);
    Task<Result> CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken);
}