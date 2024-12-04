namespace RoleRollsPocketEdition._Domain.Itens.Configurations;

public static class WeaponDefinition
{
    public static int HitDifficulty(WeaponCategory category)
    {
        switch (category)
        {
            case WeaponCategory.Light:
                return 1;
            case WeaponCategory.Medium:
                return 2;
            case WeaponCategory.Heavy:
                return 3;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }   
    public static int HitBonus(WeaponCategory category)
    {
        switch (category)
        {
            case WeaponCategory.Light:
                return 1;
            case WeaponCategory.Medium:
                return 2;
            case WeaponCategory.Heavy:
                return 3;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }   
    public static int DamageFlatBonus(WeaponCategory category)
    {
        switch (category)
        {
            case WeaponCategory.Light:
                return 1;
            case WeaponCategory.Medium:
                return 2;
            case WeaponCategory.Heavy:
                return 3;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }     
    public static int MaxDamage(WeaponCategory category)
    {
        switch (category)
        {
            case WeaponCategory.Light:
                return 4;
            case WeaponCategory.Medium:
                return 8;
            case WeaponCategory.Heavy:
                return 12;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }   
    public static int DamageBonusModifier(WeaponCategory category)
    {
        switch (category)
        {
            case WeaponCategory.Light:
                return 1;
            case WeaponCategory.Medium:
                return 2;
            case WeaponCategory.Heavy:
                return 3;
            default:
                throw new ArgumentOutOfRangeException(nameof(category), category, null);
        }
    }
}