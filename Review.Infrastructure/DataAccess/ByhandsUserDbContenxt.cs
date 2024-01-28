using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Byhands.Domain.Entities.Users;

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