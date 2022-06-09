FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 82
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/PR/PR.API/PR.API/PR.API.csproj", "src/Services/PR/PR.API/PR.API/PR.API.csproj"]
COPY ["src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "src/BuildingBlocks/EventBus/EventBus.csproj"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj"]
COPY ["src/BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "src/BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj"]
COPY ["src/Services/PR/PR.Domain/PR.Domain.csproj", "src/Services/PR/PR.Domain/PR.Domain.csproj"]
COPY ["src/Services/PR/PR.Infrastructure/PR.Infrastructure.csproj", "src/Services/PR/PR.Infrastructure/PR.Infrastructure.csproj"]

RUN dotnet restore "src/Services/PR/PR.API/PR.API/PR.API.csproj"
COPY . .

RUN dotnet build "src/Services/PR/PR.API/PR.API/PR.API.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "src/Services/PR/PR.API/PR.API/PR.API.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PR.API.dll"]
