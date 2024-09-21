using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;
internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(reservation => reservation.Id);
        builder.Property(reservation => reservation.Id).HasConversion(x => x.Id, x => new ReservationId(x));
        builder.Property(reservation => reservation.ParkingSpotId).HasConversion(spotId => spotId.Id, id => new ParkingSpotId(id));
        builder.Property(reservation => reservation.Date).HasConversion(date => date.Value, value => new Date(value));

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<CleaningReservation>(nameof(CleaningReservation))
            .HasValue<VehicleReservation>(nameof(VehicleReservation));


    }
}