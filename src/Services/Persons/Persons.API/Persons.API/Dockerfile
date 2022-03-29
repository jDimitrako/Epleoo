FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["/Services/Persons/Persons.API/Persons.API/Persons.API.csproj", "Persons.API/"]
COPY ["BuildingBlocks/EventBusRabbitMQ/EventBusRabbitMQ.csproj", "EventBusRabbitMQ/"]
COPY ["BuildingBlocks/EventBus/EventBus.csproj", "EventBus/"]
COPY ["BuildingBlocks/EventBusServiceBus/EventBusServiceBus.csproj", "EventBusServiceBus/"]
COPY ["BuildingBlocks/IntegrationEventLogEF/IntegrationEventLogEF.csproj", "IntegrationEventLogEF/"]
COPY ["BuildingBlocks/WebHost.Customization/WebHost.Customization.csproj", "WebHost.Customization/"]
COPY ["Services/Persons/Persons.Domain/Persons.Domain.csproj", "Persons.Domain/"]
COPY ["Services/Persons/Persons.Infrastructure/Persons.Infrastructure.csproj", "Persons.Infrastructure/"]
RUN dotnet restore "Services/Persons/Persons.API/Persons.API/Persons.API.csproj"
COPY . .
WORKDIR "/Persons.API"
RUN dotnet build "Persons.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Persons.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Persons.API.dll"]
