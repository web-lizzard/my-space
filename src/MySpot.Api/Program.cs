using MySpot.Application;
using MySpot.Infrastructure;
using MySpot.Core;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddCore();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration
        .WriteTo
        .Console()
        .WriteTo
        .File("logs/logs.txt")
        .WriteTo
        .Seq("http://seq:5341");
});
var app = builder.Build();
app.UseInfrastructure();
app.Run();
