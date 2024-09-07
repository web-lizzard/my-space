using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;


[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private readonly ReservationService service = new();


    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(service.FindAll());

    [HttpGet(template: "{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = service.FindById(id);

        return reservation is null ? Ok(reservation) : NotFound();
    }

    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        var id = service.Create(reservation);

        return id is not null ? CreatedAtAction(nameof(Get), new { id }, null) : BadRequest();
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation) => service.Update(id, reservation) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) => service.Delete(id) ? NoContent() : NotFound();

}

