FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG BUILDPLATFORM
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Ensure we listen on any IP Address 
ENV DOTNET_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILDPLATFORM
WORKDIR /src
COPY ["MadWorld.Backend.API/MadWorld.Backend.API.csproj", "MadWorld.Backend.API/"]
RUN dotnet restore "MadWorld.Backend.API/MadWorld.Backend.API.csproj"
COPY . .
WORKDIR "/src/MadWorld.Backend.API"
RUN dotnet build "MadWorld.Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MadWorld.Backend.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MadWorld.Backend.API.dll"]
