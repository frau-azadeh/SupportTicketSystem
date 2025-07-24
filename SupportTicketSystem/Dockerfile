# ---- Build stage ----
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY . .

# وارد فولدری شو که فایل csproj توشه
WORKDIR /src/SupportTicketSystem

RUN dotnet restore

# نصب EF Tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# اجرای migration فقط با --project چون در فولدر دقیق هستیم
RUN dotnet ef database update --project SupportTicketSystem.csproj

# خروجی رو منتشر کن
RUN dotnet publish -c Release -o /app/publish

# ---- Runtime stage ----
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .
COPY --from=build /src/SupportTicketSystem/SupportTicket.db ./SupportTicket.db

EXPOSE 80

ENTRYPOINT ["dotnet", "SupportTicketSystem.dll"]
