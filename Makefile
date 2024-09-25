
//TODO: Create command for making migration
docker compose exec server bash -c "cd src/MySpot.Infrastructure &&  dotnet ef migrations add User_Entity  --startup-project ../MySpot.Api/MySpot.Api.csproj --context MySpotDbContext -o ./DAL/Migrations"
