using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers;


[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private static Clock _clock = new();

    private static readonly ReservationService service = new ReservationService(
    [
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()), "P1" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4" ),
       new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5" ),
    ]
    );


    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(service.FindAllWeekly());

    [HttpGet(template: "{id:Guid}")]
    public ActionResult<ReservationDto> Get(Guid id)
    {
        var reservation = service.FindById(id);

        return reservation is null ? NotFound() : Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = service.Create(command with { ReservationId = Guid.NewGuid() });

        return id is not null ? CreatedAtAction(nameof(Get), new { id }, null) : BadRequest();
    }

    [HttpPut("{id:Guid}")]
    public ActionResult Put(Guid id, UpdateReservationLicensePlate command) => service.Update(command with { id = id }) ? NoContent() : NotFound();

    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id) => service.Delete(new DeleteReservation(id)) ? NoContent() : NotFound();

}

