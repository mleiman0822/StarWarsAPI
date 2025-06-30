# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["StarWarsAPI.csproj", "./"]
RUN dotnet restore "StarWarsAPI.csproj"

# Copy the rest of the source code
COPY . .

# Publish the app in Release mode
RUN dotnet publish "StarWarsAPI.csproj" -c Release -o /app/publish --no-restore

# Stage 2: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published app from build stage
COPY --from=build /app/publish .

# Expose port 80 (default HTTP port)
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "StarWarsAPI.dll"]
