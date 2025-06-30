namespace RoleRollsPocketEdition.Rolls.Services;

public interface IRollSimulationService
{
    List<CdSimulationResult> GetDc(int points, int bonus, decimal chance, IDiceRoller diceRoller);
    decimal VerifyDc(int points, int bonus, int difficulty, int complexity, IDiceRoller diceRoller);
}