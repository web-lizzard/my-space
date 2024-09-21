using MySpot.Application.Commands;
using MySpot.Application.DTO;

namespace MySpot.Application.Services;

public interface IReservationService
{
    public Task<ReservationDto?> FindById(Guid id);
    public Task<IEnumerable<ReservationDto>> FindAllWeekly();

    public Task<Guid?> ReserveForVehicle(ReserveParkingSpotForVehicle command);
    public Task ReserveForCleaning(ReserveParkingSpotForCleaning command);

    public Task<bool> Update(UpdateReservationLicensePlateForVehicle command);

    public Task<bool> Delete(DeleteReservation command);
}
