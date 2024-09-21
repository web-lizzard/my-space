FROM mcr.microsoft.com/dotnet/sdk:8.0 AS core

WORKDIR /app

COPY ./MySpot.sln ./


COPY ./src/MySpot.Core/MySpot.Core.csproj ./src/MySpot.Core/
COPY ./src/MySpot.Application/MySpot.Application.csproj ./src/MySpot.Application/
COPY ./src/MySpot.Infrastructure/MySpot.Infrastructure.csproj ./src/MySpot.Infrastructure/
COPY ./src/MySpot.Api/MySpot.Api.csproj ./src/MySpot.Api/

COPY ./tests/MySpot.Tests.Unit/MySpot.Tests.Unit.csproj ./tests/MySpot.Tests.Unit/

RUN dotnet restore 

FROM core as dev

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "watch", "--project", "src/MySpot.Api/MySpot.Api.csproj", "run", "--urls", "http://0.0.0.0:80"]

FROM core as build-prod

COPY . ./

RUN dotnet build MySpot.sln -c Release --no-restore

RUN dotnet publish ./src/MySpot.Api/MySpot.Api.csproj -c Release -o /app/out --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build-prod /app/out ./

CMD [ "dotnet", "MySpot.Api.dll", "--urls", "http://0.0.0.0:80"]
