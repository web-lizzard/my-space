using MySpot.Api.Commands;
using MySpot.Api.DTO;

namespace MySpot.Api.Services;

public interface IReservatioService
{
    public ReservationDto? FindById(Guid id);
    public IEnumerable<ReservationDto> FindAllWeekly();

    public Guid? Create(CreateReservation command);

    public bool Update(UpdateReservationLicensePlate command);

    public bool Delete(DeleteReservation command);
}
