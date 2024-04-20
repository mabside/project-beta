using Byhands.Abstractions.Entities;
using Byhands.Domain.Entities.Businesses;
using Byhands.Domain.Entities.Customers;
using Byhands.Domain.Entities.Feedbacks;
using Byhands.Domain.Entities.Links;
using Byhands.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace Byhands.Infrastructure.DataAccess;

public class ByhandsDbContext : DbContext
{
    public DbSet<Business> Businesses { get; set; } = default!;
    public DbSet<BusinessCategory> BusinessCategories { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Feedback> Feedbacks { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Link> Links { get; set; } = default!;
    public DbSet<ProductCategory> ProductCategories { get; set; } = default!;

    public ByhandsDbContext(DbContextOptions<ByhandsDbContext> options) : base(options)
    {
    }

    public ByhandsDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ByhandsDbContext).Assembly);
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

        foreach (var Product in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
        {
            if (Product.Entity is IAuditableEntity entity)
            {
                entity.CreatedOn = now;
                entity.CreatedBy = string.IsNullOrEmpty(entity.CreatedBy) ? string.Empty : entity.CreatedBy;
                entity.ModifiedBy = string.IsNullOrEmpty(entity.ModifiedBy) ? string.Empty : entity.ModifiedBy;
            }
            if (Product.Entity is IAuditableEntity newEntity) newEntity.ModifiedOn = now;
        }

        foreach (var Product in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
        {
            if (Product.Entity is IAuditableEntity entity)
            {
                entity.ModifiedOn = now;
                entity.ModifiedBy = string.IsNullOrEmpty(entity.ModifiedBy) ? string.Empty : entity.ModifiedBy;
            }
        }
    }
}
