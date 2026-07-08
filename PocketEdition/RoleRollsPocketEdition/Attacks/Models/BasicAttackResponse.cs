using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Attacks.Models;

public class BasicAttackResponse
{
    public Guid AttackerId { get; set; }
    public Guid TargetId { get; set; }
    public bool Success { get; set; }
    public Guid DefenseId { get; set; }
    public int Complexity { get; set; }
    public int Difficulty { get; set; }
    public int NumberOfSuccesses { get; set; }
    public int NumberOfRollSuccesses { get; set; }
    public int Bonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public string RolledDices { get; set; } = string.Empty;
    public EquipableSlot WeaponSlot { get; set; }
    public string WeaponName { get; set; } = string.Empty;
    public int TotalDamage { get; set; }
    public int Block { get; set; }
    public int DamageBonus { get; set; }

    public static BasicAttackResponse From(BasicAttackResult result)
    {
        return new BasicAttackResponse
        {
            AttackerId = result.Attacker.Id,
            TargetId = result.Target.Id,
            Success = result.Success,
            DefenseId = result.DefenseId,
            Complexity = result.Complexity,
            Difficulty = result.Difficulty,
            NumberOfSuccesses = result.NumberOfSuccesses,
            NumberOfRollSuccesses = result.NumberOfRollSuccesses,
            Bonus = result.Bonus,
            Luck = result.Luck,
            Advantage = result.Advantage,
            RolledDices = result.RolledDices,
            WeaponSlot = result.WeaponSlot,
            WeaponName = result.Weapon?.Name ?? string.Empty,
            TotalDamage = result.TotalDamage,
            Block = result.Block,
            DamageBonus = result.DamageBonus
        };
    }
}
