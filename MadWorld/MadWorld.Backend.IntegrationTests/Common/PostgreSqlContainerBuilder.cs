using Testcontainers.PostgreSql;

namespace MadWorld.Backend.IntegrationTests.Common;

public static class PostgreSqlContainerBuilder
{
    public static PostgreSqlContainer Build()
    {
        return new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase("db")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithCleanUp(true)
            .Build();
    }
}