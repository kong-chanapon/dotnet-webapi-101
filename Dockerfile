# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore "dotnet-api.csproj"

# Copy the rest of the application and build
COPY . ./
RUN dotnet publish "dotnet-api.csproj" -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-env /app/out .

# Entry point
ENTRYPOINT ["dotnet", "dotnet-api.dll"]
