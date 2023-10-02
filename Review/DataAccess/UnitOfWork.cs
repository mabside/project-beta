using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;
using Review.Entities.Errors;
using Review.Extensions;
using Review.Models.Bases;

namespace Review.DataAccess;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    //private IBootstrapper _bootstrapper;
    //private readonly IDomainEventDispatcher _domainEventDispatcher;

    protected readonly TContext _dbContext;

    public UnitOfWork(
        TContext context
        //IBootstrapper bootstrapper,
        //IDomainEventDispatcher domainEventDispatcher
        )
    {
        _dbContext = context;
        //_bootstrapper = bootstrapper;
        //_domainEventDispatcher = domainEventDispatcher;
    }

    //public IDbContextTransaction Begin(ICapPublisher capPublisher, bool autoCommit = false)
    //{
    //    return _dbContext.Database.BeginTransaction(capPublisher, autoCommit);
    //}

    public async Task<Result> CommitAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        try
        {
            //await _bootstrapper.BootstrapAsync(cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            //await _domainEventDispatcher.DispatchEventsAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return new Success();
        }
        catch (Exception ex) when (ex is DbUpdateException
                                      or OperationCanceledException
                                      or DbUpdateConcurrencyException)
        {
            if (ex.InnerException is PostgresException pex)
                return new ExceptionError(pex.ErrorMessage(), "Exception", false);

            //TODO: Detect and report transient error
            return new ExceptionError(ex.Message, "Exception", false);
        }
    }

    public async ValueTask DisposeAsync()
    {
        _dbContext.Dispose();
        //TODO: Handler Start/Stop Bootstrap globally
        // await _bootstrapper.DisposeAsync();
    }
}