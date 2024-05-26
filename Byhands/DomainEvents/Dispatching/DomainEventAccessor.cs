using Byhands.Abstractions.Messaging;
using Byhands.Domain;
using Microsoft.EntityFrameworkCore;

namespace RevAssure.DomainEvents.Dispatching;

public class DomainEventAccessor<TContext> : IDomainEventAccessor where TContext : DbContext
{
    private readonly TContext _dbContext;

    public DomainEventAccessor(TContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IReadOnlyCollection<IEvent> GetAllDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
                .Entries<BaseEntity<Guid>>() //this forces my hand to use Guid for all entity PK type
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        var events = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        return events;
    }

    public void ClearAllDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
                     .Entries<BaseEntity<Guid>>()
                     .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

        domainEntities
            .ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}