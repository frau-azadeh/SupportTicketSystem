# Use the official .NET SDK image to build and run the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /app

# Copy everything
COPY . ./

# Restore packages
RUN dotnet restore

# Build the project
RUN dotnet build -c Release --no-restore

# Run EF migrations (create database and tables)
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef database update

# Publish the app
RUN dotnet publish -c Release -o out

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app
COPY --from=build /app/out .

# Expose the port the app runs on
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "SupportTicketSystem.dll"]
