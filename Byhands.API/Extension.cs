using Byhands.Application.Interfaces;
using Byhands.Application.Interfaces.Providers;
using Byhands.Application.Usecases.Products.CreateProduct;
using Byhands.DataAccess;
using Byhands.Infrastructure.Brokers.Providers;
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
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUploadService, CloudinaryService>();

        return services;
    }
}