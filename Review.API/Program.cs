
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ReviewDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });

            builder.Services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<ReviewDbContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}