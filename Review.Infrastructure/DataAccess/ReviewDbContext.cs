using Microsoft.EntityFrameworkCore;

namespace Review.Infrastructure.DataAccess;

public class ReviewDbContext : DbContextBase
{
    public ReviewDbContext(DbContextOptions options) : base(options)
    {
    }

}
