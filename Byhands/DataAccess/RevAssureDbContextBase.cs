using Byhands.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Byhands.DataAccess;

public abstract class ByhandsDbContextBase : DbContext
{
    private const int Batch_Size = 1000;

    public ByhandsDbContextBase(DbContextOptions options) : base(options) { }

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

    /// <summary>
    /// NOTE: this only works for postgres DB 
    /// (as postgres DB extension is the only added version)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    //public virtual async Task InsertManyAsync<T>(IList<T> entities, CancellationToken cancellationToken) where T : class
    //{
    //    await this.BulkInsertAsync(entities, bulkAction: c =>
    //    {
    //        c.BatchSize = Batch_Size;
    //        c.SetOutputIdentity = true;
    //    },
    //    cancellationToken: cancellationToken);
    //}

    //public virtual void InsertMany<T>(IList<T> entities) where T : class
    //{
    //    this.BulkInsert(entities, bulkAction: c =>
    //    {
    //        c.BatchSize = Batch_Size;
    //        c.SetOutputIdentity = true;
    //    });
    //}

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