namespace RoleRollsPocketEdition._Domain.Rolls.Services;

public interface IRollSimulationService
{
    List<CdSimulationResult> GetDc(int points, int bonus, decimal chance);
    decimal VerifyDc(int points, int bonus, int difficulty, int complexity);
}