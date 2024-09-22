using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Repositories;
using MySpot.Infrastructure.Repositories;

namespace MySpot.Infrastructure.DAL;
internal static class Extension
{
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        var options = new DatabaseOptions();
        configuration.GetSection(DatabaseOptions.Database).Bind(options);

        var infrastructureAssembly = typeof(DatabaseOptions).Assembly;

        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        services.AddDbContext<MySpotDbContext>(x => x.UseNpgsql(options.ConnectionUrl), ServiceLifetime.Singleton);
        services.AddSingleton<IWeeklyParkingSpotRepository, SQLWeeklyParkingSpotRepository>();
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }

}