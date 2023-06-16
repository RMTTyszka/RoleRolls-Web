namespace RoleRollsPocketEdition.Rolls.Domain.Services;

public interface ICdSimulationService
{
    List<CdSimulationResult> GetDc(int points, int bonus, decimal chance);
    decimal VerifyDc(int points, int bonus, int difficulty, int complexity);
}