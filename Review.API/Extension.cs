using Review.Application.Interfaces;
using Review.Application.Usecases.Items.CreateProduct;
using Review.DataAccess;
using Review.Infrastructure.DataAccess;
using Review.Infrastructure.DataAccess.Repositories;

namespace Review.API;

public static class Extension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // TODO: register application services

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateItemCommand).Assembly);
        });

        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IBusinessRepository, BusinessRepository>();
        services.AddScoped<IBusinessCategoryRepository, BusinessCategoryRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}