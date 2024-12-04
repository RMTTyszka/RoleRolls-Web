namespace RoleRollsPocketEdition._Domain.Itens.Configurations;

public static class WeaponDefinition
{
    public static int HitDifficulty(WeaponCategory category)
    {
        return category switch
        {
            WeaponCategory.Light => 1,
            WeaponCategory.Medium => 2,
            WeaponCategory.Heavy => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }   
    public static int HitBonus(WeaponCategory category)
    {
        return category switch
        {
            WeaponCategory.Light => 1,
            WeaponCategory.Medium => 2,
            WeaponCategory.Heavy => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }   
   
    public static int MaxDamage(WeaponCategory category)
    {
        return category switch
        {
            WeaponCategory.Light => 6,
            WeaponCategory.Medium => 10,
            WeaponCategory.Heavy => 12,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }   
    public static int BaseDamageBonus(WeaponCategory category)
    {
        return category switch
        {
            WeaponCategory.Light => 2,
            WeaponCategory.Medium => 0,
            WeaponCategory.Heavy => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }  
    public static int DamageBonusModifier(WeaponCategory category)
    {
        return category switch
        {
            WeaponCategory.Light => 2,
            WeaponCategory.Medium => 3,
            WeaponCategory.Heavy => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(category), category, null)
        };
    }
}