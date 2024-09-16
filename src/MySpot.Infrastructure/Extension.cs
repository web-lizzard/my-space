using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Time;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Repositories;
using MySpot.Infrastructure.Time;

namespace MySpot.Infrastructure;
public static class Extension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IClock, Clock>();
        services.AddPostgres(configuration);
        return services;

    }
}