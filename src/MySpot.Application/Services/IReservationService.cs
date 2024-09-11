using MySpot.Application.Commands;
using MySpot.Application.DTO;

namespace MySpot.Application.Services;

public interface IReservationService
{
    public ReservationDto? FindById(Guid id);
    public IEnumerable<ReservationDto> FindAllWeekly();

    public Guid? Create(CreateReservation command);

    public bool Update(UpdateReservationLicensePlate command);

    public bool Delete(DeleteReservation command);
}
