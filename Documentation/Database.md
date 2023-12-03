# Database Readme
## Pre-requisites
```bash
dotnet tool install --global dotnet-ef
# If already installed
dotnet tool update --global dotnet-ef
```
## Migration
### Create Migration
**Default**
```bash
dotnet ef migrations add InitialCreate
```

**Extra information**
```bash
dotnet ef migrations add InitialCreate --context CurriculumVitaeContext --project ../MadWorld.Backend.Infrastructure -o ../MadWorld.Backend.Infrastructure/CurriculumVitae/Migrations
```
### Remove Migration
```bash
dotnet ef migrations remove 
```

## Local development
Create a postgres database in docker
### Create Database
```bash
docker run --name dev-madworld -p 5432:5432 -e POSTGRES_PASSWORD=mysecretpassword -d postgres
```

## References
[Postgres Docker](https://hub.docker.com/_/postgres)