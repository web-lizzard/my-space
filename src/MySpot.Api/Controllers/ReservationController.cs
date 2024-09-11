using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Services;

namespace MySpot.Api.Controllers;


[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase


{

    private readonly IReservationService Service;


    public ReservationController(IReservationService service)
    {
        Service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(Service.FindAllWeekly());

    [HttpGet(template: "{id:Guid}")]
    public ActionResult<ReservationDto> Get(Guid id)
    {
        var reservation = Service.FindById(id);

        return reservation is null ? NotFound() : Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = Service.Create(command with { ReservationId = Guid.NewGuid() });

        return id is not null ? CreatedAtAction(nameof(Get), new { id }, null) : BadRequest();
    }

    [HttpPut("{id:Guid}")]
    public ActionResult Put(Guid id, UpdateReservationLicensePlate command) => Service.Update(command with { id = id }) ? NoContent() : NotFound();

    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id) => Service.Delete(new DeleteReservation(id)) ? NoContent() : NotFound();

}

