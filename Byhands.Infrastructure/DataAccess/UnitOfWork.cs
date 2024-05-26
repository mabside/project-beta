using Byhands.Contract;
using Byhands.Entities.Errors;
using Byhands.Models.Bases;
using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace Byhands.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ByhandsDbContext _databaseContext;
    private bool _disposed;

    public UnitOfWork(ByhandsDbContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public IDbContextTransaction Begin(ICapPublisher capPublisher, bool autoCommit = false)
    {
        return _databaseContext.Database.BeginTransaction(capPublisher, autoCommit);
    }

    public async Task<Result> CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            await _databaseContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new Success();
        }
        catch (Exception ex) when (ex is PostgresException
                                      or DbUpdateException
                                      or OperationCanceledException
                                      or DbUpdateConcurrencyException)
        {
            //TODO: handle error codes
            if (ex is PostgresException x)
                return new ExceptionError(x.Message, "", false)
                    .ToFriendlyErrorMessage();

            if (ex.InnerException is PostgresException pex)
                return new ExceptionError(pex.Message, "", false)
                    .ToFriendlyErrorMessage();

            //TODO: Detect and report transient error
            return new ExceptionError(ex.Message, "", false);
        }
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _databaseContext.Dispose();
        _disposed = true;
    }
}