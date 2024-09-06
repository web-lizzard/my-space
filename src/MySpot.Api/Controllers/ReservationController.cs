using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;

namespace MySpot.Api.Controllers;


[ApiController]
[Route("reservations")]
public class ReservationController : ControllerBase
{
    private static readonly List<Reservation> reservations = [];
    private static readonly List<string> parkingSpotNames = [
        "P1", "P2", "P3", "P4", "P5"
    ];
    private static int id = 1;

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(reservations);

    [HttpGet(template: "{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = reservations.Find(reservarion => reservarion.Id == id);

        if (reservation == null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(Reservation reservation)
    {
        if (parkingSpotNames.All(name => name != reservation.ParkingSpotName))
        {
            return BadRequest();
        }
        reservation.Date = DateTime.UtcNow.AddDays(1).Date;
        var reservationsAlreadyExist = reservations.Any(name => name.ParkingSpotName == reservation.ParkingSpotName && name.Date.Date == reservation.Date.Date);

        if (reservationsAlreadyExist)
        {
            return BadRequest();
        }

        reservation.Id = id;
        id++;
        reservations.Add(reservation);
        return CreatedAtAction(nameof(Get), new { id = reservation.Id }, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        var existingReservation = reservations.Find(reservarion => reservarion.Id == id);

        if (existingReservation == null)
        {
            return NotFound();
        }

        existingReservation.LicensePlate = reservation.LicensePlate;

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var existingReservation = reservations.Find(reservarion => reservarion.Id == id);

        if (existingReservation == null)
        {
            return NotFound();
        }

        reservations.Remove(existingReservation);

        return NoContent();
    }

}

