using Microsoft.EntityFrameworkCore;
using Review.Abstractions.Entities;
using Review.Domain.Entities.Businesses;
using Review.Domain.Entities.Customers;
using Review.Domain.Entities.Feedbacks;
using Review.Domain.Entities.Items;
using Review.Domain.Entities.Links;
using Review.Domain.Entities.Spaces;

namespace Review.Infrastructure.DataAccess;

public class ReviewDbContext : DbContext
{
    public DbSet<Business> Businesses { get; set; } = default!;
    public DbSet<BusinessCategory> BusinessCategories { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Feedback> Feedbacks { get; set; } = default!;
    public DbSet<Space> Spaces { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<Link> Links { get; set; } = default!;
    public DbSet<ItemCategory> ItemCategories { get; set; } = default!;

    public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
    {
    }

    public ReviewDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ReviewDbContext).Assembly);
        base.OnModelCreating(builder);
    }

    public override int SaveChanges()
    {
        SetTimeStamps();
        //set time stamp, raise event and do other stuffs here
        return base.SaveChanges();
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        SetTimeStamps();
        //set time stamp, raise event and do other stuffs here
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SetTimeStamps();
        //set time stamp, raise event and do other stuffs here
        return base.SaveChangesAsync(cancellationToken);
    }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        SetTimeStamps();
        //set time stamp, raise event and do other stuffs here
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void SetTimeStamps()
    {
        ChangeTracker.DetectChanges();

        DateTime now = DateTime.UtcNow;

        foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (item.Entity is IAuditableEntity entity)
            {
                entity.CreatedOn = now;
                entity.CreatedBy = string.IsNullOrEmpty(entity.CreatedBy) ? string.Empty : entity.CreatedBy;
                entity.ModifiedBy = string.IsNullOrEmpty(entity.ModifiedBy) ? string.Empty : entity.ModifiedBy;
            }
            if (item.Entity is IAuditableEntity newEntity) newEntity.ModifiedOn = now;
        }

        foreach (var item in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
        {
            if (item.Entity is IAuditableEntity entity)
            {
                entity.ModifiedOn = now;
                entity.ModifiedBy = string.IsNullOrEmpty(entity.ModifiedBy) ? string.Empty : entity.ModifiedBy;
            }
        }
    }
}
