﻿using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.Mass_Transit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;


namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices
        (this IServiceCollection services,IConfiguration configuration)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehaviors<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));
        });
        services.AddFeatureManagement();
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

        return services;
    }
}
