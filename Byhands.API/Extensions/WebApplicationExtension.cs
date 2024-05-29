using Byhands.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Byhands.API.Extensions;

public static class WebApplicationExtension
{
    public static void MigrateDatabase(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        scope.ServiceProvider.GetService<ByhandsUserDbContext>()?.Database.Migrate();
    }
}
