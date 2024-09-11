# Use the official .NET SDK to build and run the app in watch mode
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS dev

# Set the working directory inside the container
WORKDIR /app

# Copy the solution and project files
COPY ./MySpot.sln ./

COPY ./src/MySpot.Core/MySpot.Core.csproj ./src/MySpot.Core/
COPY ./src/MySpot.Application/MySpot.Application.csproj ./src/MySpot.Application/
COPY ./src/MySpot.Infrastructure/MySpot.Infrastructure.csproj ./src/MySpot.Infrastructure/
COPY ./src/MySpot.Api/MySpot.Api.csproj ./src/MySpot.Api/

COPY ./tests/MySpot.Tests.Unit/MySpot.Tests.Unit.csproj ./tests/MySpot.Tests.Unit/


# Restore dependencies
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Expose the port that the Web API listens on

# Set the entrypoint to run the application in watch mode
ENTRYPOINT ["dotnet", "watch", "--project", "src/MySpot.Api/MySpot.Api.csproj", "run", "--urls", "http://0.0.0.0:80"]
