# مرحله build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# کپی پروژه
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# مرحله runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# تنظیم پورت
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "SupportTicketSystem.dll"]
