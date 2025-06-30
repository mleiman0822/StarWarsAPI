# Use Microsoft's official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["StarWarsAPI.csproj", "./"]
RUN dotnet restore "StarWarsAPI.csproj"

# Copy the rest of the source code
COPY . .

# Clean intermediate build files to avoid duplicate assembly attributes
RUN rm -rf obj bin

# Publish the app
RUN dotnet publish "StarWarsAPI.csproj" -c Release -o /app/publish --no-restore

# Use the runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80

# Start the app
ENTRYPOINT ["dotnet", "StarWarsAPI.dll"]
