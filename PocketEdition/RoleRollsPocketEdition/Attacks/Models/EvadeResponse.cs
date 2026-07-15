using RoleRollsPocketEdition.Itens;
using RoleRollsPocketEdition.Attacks.Services;
using RoleRollsPocketEdition.Creatures.Models;

namespace RoleRollsPocketEdition.Attacks.Models;

public class EvadeResponse
{
    public Guid AttackerId { get; set; }
    public Guid DefenderId { get; set; }
    public bool Success { get; set; }
    public EquipableSlot WeaponSlot { get; set; }
    public string WeaponName { get; set; } = string.Empty;
    public int BaseDice { get; set; }
    public int Difficulty { get; set; }
    public int EvadeBonus { get; set; }
    public int Luck { get; set; }
    public int Advantage { get; set; }
    public string RolledDices { get; set; } = string.Empty;
    public List<int> KeptResults { get; set; } = [];
    public List<int> Excesses { get; set; } = [];
    public int NumberOfHits { get; set; }
    public int Block { get; set; }
    public int DamageBonus { get; set; }
    public int TotalDamage { get; set; }
    public List<CreatureTakeDamageResult> VitalityDamage { get; set; } = [];

    public static EvadeResponse From(EvadeResult result)
    {
        return new EvadeResponse
        {
            AttackerId = result.Attacker.Id,
            DefenderId = result.Defender.Id,
            Success = result.Success,
            WeaponSlot = result.WeaponSlot,
            WeaponName = result.Weapon.Name ?? string.Empty,
            BaseDice = result.BaseDice,
            Difficulty = result.Difficulty,
            EvadeBonus = result.EvadeBonus,
            Luck = result.Luck,
            Advantage = result.Advantage,
            RolledDices = result.RolledDices,
            KeptResults = result.KeptResults,
            Excesses = result.Excesses,
            NumberOfHits = result.NumberOfHits,
            Block = result.Block,
            DamageBonus = result.DamageBonus,
            TotalDamage = result.TotalDamage,
            VitalityDamage = result.VitalityDamage
        };
    }
}
