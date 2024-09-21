using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;

namespace MySpot.Infrastructure.DAL.Configurations;
internal sealed class WeeklyParkingSpotConfiguration : IEntityTypeConfiguration<WeeklyParkingSpot>
{
    public void Configure(EntityTypeBuilder<WeeklyParkingSpot> builder)
    {
        builder.Property(spot => spot.Id).HasConversion(spotId => spotId.Id, id => new ParkingSpotId(id));
        builder.Property(spot => spot.Week).HasConversion(week => week.To.Value, toDate => new Week(toDate));
        builder.Property(spot => spot.Capacity).IsRequired().HasConversion(capacity => capacity.Value, value => new Capacity(value));
    }
}