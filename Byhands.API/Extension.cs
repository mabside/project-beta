using Byhands.Abstractions.Providers;
using Byhands.Application.Services;
using Byhands.Application.Usecases.Products.CreateProduct;
using Byhands.Contract;
using Byhands.Contract.Interfaces;
using Byhands.Contract.Interfaces.Auth;
using Byhands.Contract.Interfaces.Users;
using Byhands.Infrastructure.Brokers.Providers;
using Byhands.Infrastructure.Contracts.Auth;
using Byhands.Infrastructure.Contracts.Users;
using Byhands.Infrastructure.DataAccess;
using Byhands.Infrastructure.DataAccess.Repositories;
using DotNetCore.CAP.Messages;
using Microsoft.EntityFrameworkCore;
using RevAssure.DomainEvents.Dispatching;
using static Byhands.CapSettings;

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
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUploadService, CloudinaryService>();

        return services;
    }

    public static IServiceCollection AddAppProcessing<TContext>(
        this IServiceCollection services,
        string postgresDbConnectionString,
        string rabbitMQConnection,
        IConfiguration configuration)
        where TContext : DbContext
    {
        services.AddCap(options =>
        {
            try
            {
                options.UseEntityFramework<TContext>();
                options.UsePostgreSql(postgresDbConnectionString);
                options.UseRabbitMQ(rabbitMQConnection);
                options.UseDashboard(d =>
                {
                    d.PathMatch = "/cap";
                });

                options.FailedRetryCount = FAILED_RETRY_COUNT;
                options.FailedRetryInterval = FAILED_RETRY_INTERVAL_SECONDS;
                options.FailedMessageExpiredAfter = FAILED_MESSAGE_EXPIRES_AFTER;
                options.SucceedMessageExpiredAfter = SUCCEED_MESSAGE_EXPIRES_AFTER;
                options.FailedThresholdCallback = FailedThresholdCallbackSideEffect;
            }
            catch (Exception ex)
            {
                throw;
            }
        });

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IDomainEventAccessor, DomainEventAccessor<TContext>>();

        return services;
    }

    private static void FailedThresholdCallbackSideEffect(FailedInfo info)
    {
        using var scope = info.ServiceProvider.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger>();

        if (logger is ILogger log)
        {
            logger.LogCritical("{0} failed {1} after maximum attempts", info.Message.GetName(), info.MessageType);
        }
    }
}