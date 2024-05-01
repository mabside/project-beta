using Byhands.API.Constants;
using Byhands.Application.Constants;
using Byhands.Application.Interfaces;
using Byhands.Application.Interfaces.Users;
using Byhands.Application.Options;
using Byhands.DataAccess;
using Byhands.Domain.Entities.Users;
using Byhands.Infrastructure.Contracts.Users;
using Byhands.Infrastructure.DataAccess;
using Byhands.Infrastructure.DataAccess.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;

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

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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