namespace MadWorld.Backend.Domain.CurriculaVitae;

public interface ICurriculaVitaeRepository
{
    Profile GetProfile(bool isDraft);
}