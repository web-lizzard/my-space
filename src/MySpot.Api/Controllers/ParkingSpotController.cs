using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Abstractions;
using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Security;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.Controllers;


[ApiController]
[Route("parking-spots")]
public class ParkingSpotController(
    ICommandHandler<ReserveParkingSpotForVehicle> reserveParkingSpotForVehicleHandler,
    IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsHandler,
    ICommandHandler<DeleteReservation> deleteReservationHandler,
    ICommandHandler<UpdateReservationLicensePlateForVehicle> updateReservationLicensePlateHandler,
    ICommandHandler<ReserveParkingSpotForCleaning> reserveParkingSpotForCleaningHandler) : ControllerBase
{
    private readonly ICommandHandler<ReserveParkingSpotForVehicle> _reserveParkingSpotForVehicleHandler = reserveParkingSpotForVehicleHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaning> _reserveParkingSpotForCleaningHandler = reserveParkingSpotForCleaningHandler;
    private readonly ICommandHandler<UpdateReservationLicensePlateForVehicle> _updateReservationLicensePlateHandler = updateReservationLicensePlateHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler = deleteReservationHandler;
    private readonly IQueryHandler<GetWeeklyParkingSpots, IEnumerable<WeeklyParkingSpotDto>> _getWeeklyParkingSpotsHandler = getWeeklyParkingSpotsHandler;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpots query)
    {
        return Ok(await _getWeeklyParkingSpotsHandler.Handle(query));
    }

    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    [Authorize]
    async public Task<ActionResult> Post(Guid parkingSpotId, ReserveParkingSpotForVehicle command)
    {

        var reservationId = Guid.NewGuid();
        await _reserveParkingSpotForVehicleHandler.Handle(command with
        {
            ReservationId = reservationId,
            ParkingSpotId = parkingSpotId,
            UserId = Guid.Parse(User.Identity.Name),
            JobTitle = User.Claims.SingleOrDefault(claim => claim.Type == ClaimsType.JobTitleClaim).Value,
            EmployeeName = User.Claims.SingleOrDefault(claim => claim.Type == ClaimsType.FullNameClaim).Value

        });
        return Created();
    }

    [HttpPost("reservations/cleaning")]
    async public Task<ActionResult> Post(ReserveParkingSpotForCleaning command)
    {
        await _reserveParkingSpotForCleaningHandler.Handle(command);

        return Created();
    }

    [HttpPut("reservations/{id:Guid}")]
    public async Task<ActionResult> Put(Guid id, UpdateReservationLicensePlateForVehicle command)
    {
        await _updateReservationLicensePlateHandler.Handle(command with { Id = id });
        return NoContent();
    }

    [HttpDelete("reservations/{id:Guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _deleteReservationHandler.Handle(new DeleteReservation(id));
        return NoContent();
    }

}

