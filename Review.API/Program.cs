using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using Review.API.Constants;
using Review.Domain.Entities.Users;
using Review.Infrastructure.DataAccess;

namespace Review.API
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

            builder.Services.AddDbContext<ReviewDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });
            builder.Services.AddDbContext<ReviewUserDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });
            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ReviewUserDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            app.UseFastEndpoints()
                .UseSwaggerGen();

            app.Run();
        }
    }
}