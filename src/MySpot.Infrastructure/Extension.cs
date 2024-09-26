using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;
using MySpot.Core.Time;
using MySpot.Infrastructure.Auth;
using MySpot.Infrastructure.DAL;
using MySpot.Infrastructure.DAL.Decorators;
using MySpot.Infrastructure.Exceptions;
using MySpot.Infrastructure.Logging.Decorators;
using MySpot.Infrastructure.Time;

namespace MySpot.Infrastructure;
public static class Extension
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IClock, Clock>();
        services.AddPostgres(configuration);
        services.AddAuth(configuration);
        services.AddSingleton<ExceptionMiddleware>();
        services.AddSingleton<IUnitOfWork, SQLUnitOfWork>();
        services.TryDecorate(typeof(ICommandHandler<>), typeof(LoggingHandlerDecorator<>));
        services.TryDecorate(typeof(ICommandHandler<>), typeof(UnitOfWorkHandlerDecorator<>));
        services.AddHttpContextAccessor();
        services.AddSecurity();

        return services;

    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}