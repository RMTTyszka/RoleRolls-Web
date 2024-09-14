namespace RoleRollsPocketEdition._Domain.Rolls.Services;

public class CdSimulationResult {
    public int Difficulty { get; set; }
    public int Complexity { get; set; }
    public decimal Chance { get; set; }

    public CdSimulationResult(int difficulty, int complexity, decimal chance) {
        Difficulty = difficulty;
        Complexity = complexity;
        Chance = chance;
    }
}