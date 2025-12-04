using System;
using RoleRollsPocketEdition.Itens;

namespace RoleRollsPocketEdition.Itens.Configurations;

public static class ArmorDefinition
{
    private const int BaseComplexity = 10;

    public static int GetTier(int level) => 1 + Math.Max(level - 1, 0) / 2;

    // AKA dodge/evasion: usado na complexidade do ataque.
    public static int DefenseBonus1(ArmorCategory armorCategory) => armorCategory switch
    {
        ArmorCategory.None => 0,
        ArmorCategory.Light => 2,
        ArmorCategory.Medium => 1,
        ArmorCategory.Heavy => -1,
        _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
    };

    // Mantido para compatibilidade; hoje seguimos a tabela de dodge.
    public static int DefenseBonus2(ArmorCategory armorCategory) => DefenseBonus1(armorCategory);

    // Bônus incremental de block por tier (light=1, medium=2, heavy=3).
    public static int DamageReductionByLevel(ArmorCategory armorCategory) => armorCategory switch
    {
        ArmorCategory.None => 0,
        ArmorCategory.Light => 1,
        ArmorCategory.Medium => 2,
        ArmorCategory.Heavy => 3,
        _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
    };

    // Block base por categoria (light=2, medium=4, heavy=4).
    public static int BaseDamageReduction(ArmorCategory armorCategory) => armorCategory switch
    {
        ArmorCategory.None => 0,
        ArmorCategory.Light => 2,
        ArmorCategory.Medium => 4,
        ArmorCategory.Heavy => 4,
        _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
    };

    // Block total: base + (perTier * tier).
    public static int TotalBlock(ArmorCategory armorCategory, int level)
    {
        var tier = GetTier(level);
        return BaseDamageReduction(armorCategory) + DamageReductionByLevel(armorCategory) * tier;
    }

    public static int BaseLuck(ArmorCategory armorCategory) => 0;

    public static int BaseComplexityWithArmor(ArmorCategory armorCategory, int defenderLevel) =>
        BaseComplexity + DefenseBonus1(armorCategory) + Math.Max(defenderLevel - 1, 0);
}
