using MySpot.Application.Commands;
using MySpot.Application.DTO;

namespace MySpot.Application.Services;

public interface IReservationService
{
    public Task<ReservationDto?> FindById(Guid id);
    public Task<IEnumerable<ReservationDto>> FindAllWeekly();

    public Task<Guid?> Create(CreateReservation command);

    public Task<bool> Update(UpdateReservationLicensePlate command);

    public Task<bool> Delete(DeleteReservation command);
}
