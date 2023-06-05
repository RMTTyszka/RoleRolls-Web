namespace RoleRollsPocketEdition.Rolls.Application;

public class CdDiscoveryResult {
    public int difficulty;
    public int complexity;
    public decimal chance;

    public CdDiscoveryResult(int difficulty, int complexity, decimal chance) {
        this.difficulty = difficulty;
        this.complexity = complexity;
        this.chance = chance;
    }
}