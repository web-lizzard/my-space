using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Services;

namespace MySpot.Api.Controllers;


[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService service = new();


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

