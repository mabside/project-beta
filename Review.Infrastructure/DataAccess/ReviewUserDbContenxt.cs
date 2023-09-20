using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Review.Domain.Entities.Customers;
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