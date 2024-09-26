using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Abstractions;

namespace MySpot.Application;
public static class Extension
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ICommandHandler<>).Assembly;


        services.Scan(s => s.FromAssemblies(applicationAssembly)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithSingletonLifetime());

        return services;

    }
}