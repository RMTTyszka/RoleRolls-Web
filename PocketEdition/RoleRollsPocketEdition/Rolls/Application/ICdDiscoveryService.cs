namespace RoleRollsPocketEdition.Rolls.Application;

public interface ICdDiscoveryService
{
    List<CdDiscoveryResult> GetDc(int points, int bonus, decimal chance);
    decimal VerifyDc(int points, int bonus, int difficulty, int complexity);
}