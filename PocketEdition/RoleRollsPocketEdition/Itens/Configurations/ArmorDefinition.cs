namespace RoleRollsPocketEdition.Itens.Configurations;

public static class ArmorDefinition
{
    // AKA  evasion / defense bonus against being hit
    public static int DefenseBonus1(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 3,
            ArmorCategory.Light => 2,
            ArmorCategory.Medium => 0,
            ArmorCategory.Heavy => -1,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }

    public static int DefenseBonus2(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => -1,
            ArmorCategory.Light => 0,
            ArmorCategory.Medium => 2,
            ArmorCategory.Heavy => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }

    // AKA defense
    public static int DamageReductionByLevel(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 0,
            ArmorCategory.Light => 1,
            ArmorCategory.Medium => 2,
            ArmorCategory.Heavy => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }    
    // AKA defense
    public static int BaseDamageReduction(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 0,
            ArmorCategory.Light => 1,
            ArmorCategory.Medium => 2,
            ArmorCategory.Heavy => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }  
    public static int BaseLuck(ArmorCategory armorCategory)
    {
        return armorCategory switch
        {
            ArmorCategory.None => 0,
            ArmorCategory.Light => 0,
            ArmorCategory.Medium => 0,
            ArmorCategory.Heavy => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(armorCategory), armorCategory, null)
        };
    }
}
