using MySpot.Core.ValueObjects;

namespace MySpot.Core.Entities;
public sealed class VehicleReservation : Reservation
{
    public VehicleReservation(Guid id, Guid parkingSpotId, EmployeeName employeeName, LicensePlate licensePlate, Date date, Capacity capacity, UserId userId) : base(id, parkingSpotId, date, capacity)
    {
        EmployeeName = employeeName;
        UserId = userId;
        ChangeLicensePlate(licensePlate);
    }

    private VehicleReservation() { }
    public EmployeeName EmployeeName { get; private set; }

    public LicensePlate LicensePlate { get; private set; }

    public UserId UserId { get; private set; }


    public void ChangeLicensePlate(LicensePlate licensePlate)
    {
        LicensePlate = licensePlate;
    }
}