using Microsoft.Extensions.DependencyInjection;
using MySpot.Core.Policies;
using MySpot.Core.Services;

namespace MySpot.Core;

public static class Extension
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IReservationPolicy, RegularEmployeeReservationPolicy>();
        services.AddSingleton<IReservationPolicy, ManagerReservationPolicy>();
        services.AddSingleton<IReservationPolicy, BossReservationPolicy>();
        services.AddSingleton<IParkingReservationService, ParkingReservationService>();

        return services;
    }
}