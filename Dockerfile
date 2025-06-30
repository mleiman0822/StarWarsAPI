# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file(s) and restore dependencies
COPY ["StarWarsAPI.csproj", "./"]
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish the app in Release mode
RUN dotnet publish "StarWarsAPI.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the published output from build stage
COPY --from=build /app/publish .

# Expose port 80 for HTTP traffic
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "StarWarsAPI.dll"]
