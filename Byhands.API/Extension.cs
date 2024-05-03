using Byhands.Application.Interfaces;
using Byhands.Application.Interfaces.Providers;
using Byhands.Application.Interfaces.Users;
using Byhands.Application.Usecases.Products.CreateProduct;
using Byhands.DataAccess;
using Byhands.Infrastructure.Brokers.Providers;
using Byhands.Infrastructure.Contracts.Auth;
using Byhands.Infrastructure.Contracts.Users;
using Byhands.Infrastructure.DataAccess;
using Byhands.Infrastructure.DataAccess.Repositories;

namespace Byhands.API;

public static class Extension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // TODO: register application services

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductCategoryRepository, productCategoryRepository>();
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUploadService, CloudinaryService>();

        return services;
    }
}