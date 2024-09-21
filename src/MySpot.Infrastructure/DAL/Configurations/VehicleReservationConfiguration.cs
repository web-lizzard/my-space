using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;

internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.Property(reservation => reservation.LicensePlate).HasConversion(plate => plate.Value, value => new LicensePlate(value));
        builder.Property(reservation => reservation.EmployeeName).HasConversion(name => name.Value, name => new EmployeeName(name));

    }
}