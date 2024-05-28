
using System.Text;
using Byhands.API.Constants;
using Byhands.Application.Utils;
using Byhands.Domain.Entities.Users;
using Byhands.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Byhands.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;

            var jwtOptionsSection = configuration.GetSection("JWTOptions");

            builder.Services.Configure<JWTOptions>(jwtOptionsSection);

            //builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options => {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,
            //            ValidIssuer = builder.Configuration["JWTOptions:Issuer"],
            //            ValidAudience = builder.Configuration["JWTOptions:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTOptions:Key"]!))
            //        };
            //    });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddApplication();

            builder.Services.AddDbContext<ByhandsDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp)!);
            });

            builder.Services.AddDbContext<ByhandsUserDbContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString(KeyConstants.DBConnectionProp)!);
            });

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ByhandsUserDbContext>();

            builder.Services.AddAppProcessing<ByhandsDbContext>(
                postgresDbConnectionString: builder.Configuration.GetConnectionString("ByhandsDBConnection")!,
                rabbitMQConnection: "localhost",
                configuration: builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}