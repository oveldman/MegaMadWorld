FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG BUILDPLATFORM
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILDPLATFORM
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MadWorld.ShipSimulator.gRPC/MadWorld.ShipSimulator.gRPC.csproj", "MadWorld.ShipSimulator.gRPC/"]
RUN dotnet restore "MadWorld.ShipSimulator.gRPC/MadWorld.ShipSimulator.gRPC.csproj"
COPY . .
WORKDIR "/src/MadWorld.ShipSimulator.gRPC"
RUN dotnet build "MadWorld.ShipSimulator.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MadWorld.ShipSimulator.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadWorld.ShipSimulator.gRPC.dll"]
