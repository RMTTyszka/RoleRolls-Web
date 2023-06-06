namespace RoleRollsPocketEdition.Rolls.Domain.Services;

public class CdDiscoveryResult {
    public int Difficulty { get; set; }
    public int Complexity { get; set; }
    public decimal Chance { get; set; }

    public CdDiscoveryResult(int difficulty, int complexity, decimal chance) {
        Difficulty = difficulty;
        Complexity = complexity;
        Chance = chance;
    }
}