FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/Persons/Persons.API/Persons.API/Persons.API.csproj", "Persons.API/"]
COPY ["src/BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["src/BuildingBlocks/EventBus/EventBus.csproj", "EventBus/"]
COPY ["src/BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "EventBusServiceBus/"]
COPY ["src/BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "IntegrationEventLogEF/"]
COPY ["src/BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "WebHost.Customization/"]
COPY ["src/Services/Persons/Persons.Domain/Persons.Domain.csproj", "Persons.Domain/"]
COPY ["src/Services/Persons/Persons.Infrastructure/Persons.Infrastructure.csproj", "Persons.Infrastructure/"]
RUN dotnet restore "src/Services/Persons/Persons.API/Persons.API/Persons.API.csproj"
COPY . .
WORKDIR "/src/Persons.API"
RUN dotnet build "Persons.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Persons.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Persons.API.dll"]
