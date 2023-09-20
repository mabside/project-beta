using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Review.Infrastructure.DataAccess;

public class DbContextBase : DbContext
{
    public DbContextBase(DbContextOptions options) : base(options) { }

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
{
}
