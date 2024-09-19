using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Time;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Time;

namespace MySpot.Infrastructure;
public static class Extension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IClock, Clock>();
        services.AddPostgres(configuration);
        services.AddSingleton<ExceptionMiddleware>();
        return services;

    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        // app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();

        return app;
    }
}