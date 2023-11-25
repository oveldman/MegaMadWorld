using MadWorld.Backend.Application.CurriculaVitae;
using MadWorld.Backend.Domain.CommonExceptions;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.CurriculaVitae;

namespace MadWorld.Backend.Unittests.Application.CurriculaVitae;

public class GetCurriculumVitaeUseCaseTests
{
    [Fact]
    public void GetCurriculumVitae_WhenRequestIsNull_ThrowsValidationException()
    {
        // Arrange
        var curriculaVitaeRepository = Substitute.For<ICurriculaVitaeRepository>();
        var useCase = new GetCurriculumVitaeUseCase(curriculaVitaeRepository);
        
        // Act & Assert
        Should.Throw<ValidationException>(() => useCase.GetCurriculumVitae(null!));
    }
    
    [Fact]
    public void GetCurriculumVitae_WhenProfileIsNotFound_ThrowsEntityNotFoundException()
    {
        // Arrange
        var curriculaVitaeRepository = Substitute.For<ICurriculaVitaeRepository>();
        curriculaVitaeRepository
            .GetProfile(Arg.Any<bool>())
            .Returns((Profile)null!);
        
        var useCase = new GetCurriculumVitaeUseCase(curriculaVitaeRepository);

        var request = new GetCurriculumVitaeRequest();
        
        // Act & Assert
        Should.Throw<EntityNotFoundException>(() => useCase.GetCurriculumVitae(request));
    }
}