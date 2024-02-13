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
COPY ["MadWorld.Backend.gRPC/MadWorld.Backend.gRPC.csproj", "MadWorld.Backend.gRPC/"]
RUN dotnet restore "MadWorld.Backend.gRPC/MadWorld.Backend.gRPC.csproj"
COPY . .
WORKDIR "/src/MadWorld.Backend.gRPC"
RUN dotnet build "MadWorld.Backend.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "MadWorld.Backend.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadWorld.Backend.gRPC.dll"]
