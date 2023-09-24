using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities.Users;

namespace Review.Infrastructure.DataAccess;

public class ReviewUserDbContext : IdentityDbContext<User, Role, string>
{

    public ReviewUserDbContext(DbContextOptions<ReviewUserDbContext> options) : base(options)
    {
    }

    public ReviewUserDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}