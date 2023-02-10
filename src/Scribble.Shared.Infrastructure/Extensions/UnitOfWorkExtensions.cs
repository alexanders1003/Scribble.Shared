using Microsoft.Extensions.DependencyInjection;
using Scribble.Posts.Infrastructure.Factories;
using Scribble.Shared.Infrastructure.Factories;

namespace Scribble.Shared.Infrastructure.Extensions;

public static class UnitOfWorkExtensions
{
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<IConnectionFactory, ConnectionFactory>();
        services.AddScoped<IUnitOfWorkFactory, UnitOfWorkFactory>();
        
        return services;
    }
}