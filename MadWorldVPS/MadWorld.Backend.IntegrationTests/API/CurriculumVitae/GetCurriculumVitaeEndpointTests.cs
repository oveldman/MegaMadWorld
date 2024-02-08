using System.Net.Http.Json;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Infrastructure.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using MadWorld.Backend.API;
using MadWorld.Backend.IntegrationTests.Common;

namespace MadWorld.Backend.IntegrationTests.API.CurriculumVitae;

public sealed class GetCurriculumVitaeEndpointTests : ApiTestBase
{
    public GetCurriculumVitaeEndpointTests(WebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Execute_WhenRequestIsValid_ShouldReturnValidResponse()
    {
        // Arrange
        var profile = Profile.Create("Emily Thompson", "Graphic Designer");
        using (var scope = Factory.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<CurriculaVitaeContext>();
            await context.Profiles.AddAsync(profile);
            await context.SaveChangesAsync();
        }

        var client = Factory.CreateClient();
        // Act
        var response = await client.GetAsync("/CurriculumVitae?isdraft=false");

        // Assert
        response.EnsureSuccessStatusCode();
        var result = await response.Content
            .ReadFromJsonAsync<GetCurriculumVitaeResponse>();
        result!.Profile.Id.ShouldBe(profile.Id);
        result!.Profile.FullName.ShouldBe("Emily Thompson");
        result!.Profile.JobTitle.ShouldBe("Graphic Designer");
    }
}