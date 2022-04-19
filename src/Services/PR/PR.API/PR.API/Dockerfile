FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/PR/PR.API/PR.API/PR.API.csproj", "PR.API/"]
COPY ["src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "EventBus/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "EventBusServiceBus/"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "IntegrationEventLogEF/"]
COPY ["src/BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "WebHost.Customization/"]
COPY ["src/Services/PR/PR.Domain/PR.Domain.csproj", "PR.Domain/"]
COPY ["src/Services/PR/PR.Infrastructure/PR.Infrastructure.csproj", "PR.Infrastructure/"]
RUN dotnet restore "src/Services/PR/PR.API/PR.API/PR.API.csproj"
COPY . .
WORKDIR "/src/PR.API"
RUN dotnet build "PR.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PR.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PR.API.dll"]
