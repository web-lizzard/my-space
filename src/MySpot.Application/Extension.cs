using Microsoft.Extensions.DependencyInjection;
using MySpot.Application.Services;

namespace MySpot.Application;
public static class Extension
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IReservationService, ReservationService>();
        return services;

    }
}