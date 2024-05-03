using System.Text;
using Byhands.API.Constants;
using Byhands.Application.Constants;
using Byhands.Application.Interfaces;
using Byhands.Application.Interfaces.Users;
using Byhands.Application.Options;
using Byhands.Application.Utils;
using Byhands.DataAccess;
using Byhands.Domain.Entities.Users;
using Byhands.Infrastructure.Contracts.Auth;
using Byhands.Infrastructure.Contracts.Users;
using Byhands.Infrastructure.DataAccess;
using Byhands.Infrastructure.DataAccess.Repositories;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Byhands.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var configuration = builder.Configuration;
            // Add services to the container.

            builder.Services
                .AddFastEndpoints()
                .AddAuthorization();

            var jwtOptionsSection = configuration.GetSection("JWTOptions");

            builder.Services.Configure<JWTOptions>(jwtOptionsSection);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApplication();

            builder.Services.AddDbContext<ByhandsDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });

            builder.Services.AddDbContext<ByhandsUserDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(KeyConstants.DBConnectionProp));
            });

            builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ByhandsUserDbContext>()
                .AddDefaultTokenProviders();

            var secretKey = configuration["JWT:Key"] ?? throw new ArgumentNullException();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Wedding Planner API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection(Sections.CLOUDINARY));

            var app = builder.Build();

            app.UseFastEndpoints()
                .UseSwaggerGen();

            app.Run();
        }
    }
}