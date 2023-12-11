FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Ensure we listen on any IP Address 
ENV DOTNET_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MadWorld.ShipSimulator.API/MadWorld.ShipSimulator.API.csproj", "MadWorld.ShipSimulator.API/"]
RUN dotnet restore "MadWorld.ShipSimulator.API/MadWorld.ShipSimulator.API.csproj"
COPY . .
WORKDIR "/src/MadWorld.ShipSimulator.API"
RUN dotnet build "MadWorld.ShipSimulator.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MadWorld.ShipSimulator.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadWorld.ShipSimulator.API.dll"]
