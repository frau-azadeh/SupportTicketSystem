# ---- Build Stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN dotnet ef database update
RUN dotnet publish -c Release -o /app/publish

# ---- Runtime Stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

# کپی اپلیکیشن و دیتابیس
COPY --from=build /app/publish . 
COPY --from=build /src/SupportTicket.db ./SupportTicket.db

EXPOSE 80

ENTRYPOINT ["dotnet", "SupportTicketSystem.dll"]
