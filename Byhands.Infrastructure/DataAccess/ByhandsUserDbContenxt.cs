using Byhands.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Byhands.Infrastructure.DataAccess;

public class ByhandsUserDbContext : IdentityDbContext<User, Role, string>
{

    public ByhandsUserDbContext(DbContextOptions<ByhandsUserDbContext> options) : base(options)
    {
    }

    public ByhandsUserDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}