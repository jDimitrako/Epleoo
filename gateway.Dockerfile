FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator.csproj", "src/ApiGateways/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator.csproj"]
RUN dotnet restore "src/ApiGateways/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator.csproj"
COPY . .
WORKDIR "/src/Web.MainApp.HttpAggregator"
RUN dotnet build "src/ApiGateways/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ApiGateways/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator/Web.MainApp.HttpAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.MainApp.HttpAggregator.dll"]
