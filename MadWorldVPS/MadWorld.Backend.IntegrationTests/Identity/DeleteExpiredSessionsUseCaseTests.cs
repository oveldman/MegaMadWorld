using MadWorld.Backend.Identity;
using MadWorld.Backend.Identity.Application;
using MadWorld.Backend.Identity.Domain.Users;
using MadWorld.Backend.Identity.Infrastructure;
using MadWorld.Backend.IntegrationTests.Common;
using MadWorld.Shared.Common.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;

namespace MadWorld.Backend.IntegrationTests.Identity;

public class DeleteExpiredSessionsUseCaseTests : IdentityTestBase
{
    private readonly IDateTimeProvider _dateTimeProvider;
    
    public DeleteExpiredSessionsUseCaseTests(WebApplicationFactory<Program> factory) : base(factory)
    {
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
    }

    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        var dateToday = new DateTime(2024, 01, 28, 0, 0,0,0, DateTimeKind.Utc);
        
        _dateTimeProvider.UtcNow().Returns(dateToday);

        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
            var user = context.Users.First();

            await context.RefreshTokens.AddAsync(new RefreshToken("Test", "https://test", new DateTime(2024, 01, 27, 0, 0,0,0, DateTimeKind.Utc), user.Id));
            await context.RefreshTokens.AddAsync(new RefreshToken("Test2", "https://test", new DateTime(2024, 01, 29, 0, 0,0,0, DateTimeKind.Utc), user.Id));

            await context.SaveChangesAsync();
        }

        using (var scope = Factory.Services.CreateScope())
        {
            var useCase = scope.ServiceProvider.GetRequiredService<DeleteExpiredSessionsUseCase>();
            
            // Act
            await useCase.DeleteExpiredSessions();
        }

        // Assert
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

            var result = context
                .RefreshTokens
                .AsNoTracking()
                .ToList();
            
            result.Count.ShouldBe(1);
            result[0].Token.ShouldBe("Test2");
        }
    }

    protected override void SetupServices(IServiceCollection services)
    {
        services.AddSingleton(_dateTimeProvider);
        
        base.SetupServices(services);
    }
}