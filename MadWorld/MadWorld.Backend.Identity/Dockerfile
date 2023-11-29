FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MadWorld.Backend.Identity/MadWorld.Backend.Identity.csproj", "MadWorld.Backend.Identity/"]
RUN dotnet restore "MadWorld.Backend.Identity/MadWorld.Backend.Identity.csproj"
COPY . .
WORKDIR "/src/MadWorld.Backend.Identity"
RUN dotnet build "MadWorld.Backend.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MadWorld.Backend.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadWorld.Backend.Identity.dll"]
