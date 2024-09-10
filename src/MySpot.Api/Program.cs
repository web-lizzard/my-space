using MySpot.Api.Repositories;
using MySpot.Api.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IClock, Clock>();
builder.Services.AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklingParkingSpotRepository>();
builder.Services.AddSingleton<IReservatioService, ReservationService>();
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();

app.Run();
