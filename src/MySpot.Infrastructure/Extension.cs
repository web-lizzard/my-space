using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Time;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Repositories;
using MySpot.Infrastructure.Time;

namespace MySpot.Infrastructure;
public static class Extension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IClock, Clock>();
        services.AddPostgres();
        return services;

    }
}