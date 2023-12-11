FROM nginx:latest AS base
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MadWorld.ShipSimulator.UI/MadWorld.ShipSimulator.UI.csproj", "MadWorld.ShipSimulator.UI/"]
RUN dotnet restore "MadWorld.ShipSimulator.UI/MadWorld.ShipSimulator.UI.csproj"
COPY . .
WORKDIR "/src/MadWorld.ShipSimulator.UI"
RUN dotnet build "MadWorld.ShipSimulator.UI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MadWorld.ShipSimulator.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html