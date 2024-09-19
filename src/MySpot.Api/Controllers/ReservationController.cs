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
    async public Task<ActionResult<IEnumerable<ReservationDto>>> Get() => Ok(await Service.FindAllWeekly());

    [HttpGet(template: "{id:Guid}")]
    async public Task<ActionResult<ReservationDto>> Get(Guid id)
    {
        var reservation = await Service.FindById(id);

        return reservation is null ? NotFound() : Ok(reservation);
    }

    [HttpPost]
    async public Task<ActionResult> Post(CreateReservation command)
    {
        var id = await Service.Create(command with { ReservationId = Guid.NewGuid() });

        return id is not null ? CreatedAtAction(nameof(Get), new { id }, null) : BadRequest();
    }

    [HttpPut("{id:Guid}")]
    public async Task<ActionResult> Put(Guid id, UpdateReservationLicensePlate command) => await Service.Update(command with { id = id }) ? NoContent() : NotFound();

    [HttpDelete("{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id) => await Service.Delete(new DeleteReservation(id)) ? NoContent() : NotFound();

}

