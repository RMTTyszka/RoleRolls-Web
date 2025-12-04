using System;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Itens.Configurations;

public static class WeaponDefinition
{
    public record WeaponProfile(int Difficulty, int HitBonus, int DamageBonusPerHit);

    public static int GetTier(int level) => 1 + Math.Max(level - 1, 0) / 2;

    public static WeaponProfile GetProfile(WeaponCategory category, int level)
    {
        var tier = GetTier(level);
        return category switch
        {
            WeaponCategory.Light => new WeaponProfile(Difficulty: 1, HitBonus: 1, DamageBonusPerHit: tier * 3),
            WeaponCategory.Medium => new WeaponProfile(Difficulty: 2, HitBonus: 0, DamageBonusPerHit: tier * 5),
            WeaponCategory.Heavy => new WeaponProfile(Difficulty: 3, HitBonus: 0, DamageBonusPerHit: tier * 6 + 2),
            WeaponCategory.LightShield => new WeaponProfile(Difficulty: 1, HitBonus: 0, DamageBonusPerHit: 0),
            WeaponCategory.MediumShield => new WeaponProfile(Difficulty: 2, HitBonus: 0, DamageBonusPerHit: 0),
            WeaponCategory.HeavyShield => new WeaponProfile(Difficulty: 3, HitBonus: 0, DamageBonusPerHit: 0),
            _ => new WeaponProfile(Difficulty: 1, HitBonus: 0, DamageBonusPerHit: 0)
        };
    }
}
