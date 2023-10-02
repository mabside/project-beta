using Microsoft.Extensions.DependencyInjection;
using Review.Application.Usecases.Items.CreateProduct;

namespace Review.Application;

public static class Extension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // TODO: register application services

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateItemCommand).Assembly);
        });

        return services;
    }
}