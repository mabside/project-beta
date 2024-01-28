using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Byhands.API.Constants;
using Byhands.Application.Constants;
using Byhands.Application.Options;
using Byhands.Domain.Entities.Users;
using Byhands.Infrastructure.DataAccess;

namespace Byhands.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services
                .AddFastEndpoints()
                .AddSwaggerDocument();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplication();

            builder.Services.AddDbContext<ByhandsDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });

            builder.Services.AddDbContext<ByhandsUserDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ByhandsUserDbContext>();

            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection(Sections.CLOUDINARY));

            var app = builder.Build();

            app.UseFastEndpoints()
                .UseSwaggerGen();

            app.Run();
        }
    }
}