using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.Repositories;

namespace MySpot.Infrastructure.DAL;
internal static class Extension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services)
    {
        const string CONNECTION_URL = "Host=postgres;Database=my-spot;Username=postgres;Password=example";
        services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(CONNECTION_URL), ServiceLifetime.Singleton);
        services.AddSingleton<IWeeklyParkingSpotRepository, SQLWeeklyParkingSpotRepository>();
        services.AddHostedService<DatabaseInitilizer>();

        return services;
    }

}