using Microsoft.EntityFrameworkCore;
using Review.Abstractions.Entities;
using Review.Domain.Entities.Businesses;
using Review.Domain.Entities.Customers;

namespace Review.Infrastructure.DataAccess;

public class ReviewDbContext : DbContext
{
    public DbSet<Business> Businesses { get; set; } = default!;
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;
    public DbSet<Domain.Entities.Reviews.Feedback> Reviews { get; set; } = default!;

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
        return base.SaveChanges();
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
