namespace RoleRollsPocketEdition._Domain.Creatures.Models;

public class DamageRollResult
{
    public int DiceValue { get; set; }
    public int FlatBonus { get; set; }
    public int BonusModifier { get; set; }
    public int TotalDamage { get; set; }
    public int ReducedDamage { get; set; }
}