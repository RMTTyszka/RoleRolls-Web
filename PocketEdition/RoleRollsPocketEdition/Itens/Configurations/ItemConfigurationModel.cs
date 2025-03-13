using RoleRollsPocketEdition.Core.Entities;

namespace RoleRollsPocketEdition.Itens.Configurations;

public class ItemConfigurationModel
{
    public Property? ArmorProperty { get; set; }
    public Property? MeleeLightWeaponHitProperty { get; set; }
    public Property? MeleeMediumWeaponHitProperty { get; set; }
    public Property? MeleeHeavyWeaponHitProperty { get; set; }   
    public Property? MeleeLightWeaponDamageProperty { get; set; }
    public Property? MeleeMediumWeaponDamageProperty { get; set; }
    public Property? MeleeHeavyWeaponDamageProperty { get; set; }  
    public Property? RangedLightWeaponHitProperty { get; set; }
    public Property? RangedMediumWeaponHitProperty { get; set; }
    public Property? RangedHeavyWeaponHitProperty { get; set; }   
    public Property? RangedLightWeaponDamageProperty { get; set; }
    public Property? RangedMediumWeaponDamageProperty { get; set; }
    public Property? RangedHeavyWeaponDamageProperty { get; set; }
    public Property? BasicAttackTargetFirstVitality { get; set; }

    public static ItemConfigurationModel FromConfiguration(ItemConfiguration? templateItemConfiguration)
    {
        if (templateItemConfiguration == null)
        {
            return new ItemConfigurationModel();
        }

        return new ItemConfigurationModel
        {
            ArmorProperty = templateItemConfiguration.ArmorProperty,
            BasicAttackTargetFirstVitality = templateItemConfiguration.BasicAttackTargetFirstVitality,
            BasicAttackTargetSecondVitality = templateItemConfiguration.BasicAttackTargetSecondVitality,
            MeleeLightWeaponHitProperty = templateItemConfiguration.MeleeLightWeaponHitProperty,
            MeleeMediumWeaponHitProperty = templateItemConfiguration.MeleeMediumWeaponHitProperty,
            MeleeHeavyWeaponHitProperty = templateItemConfiguration.MeleeHeavyWeaponHitProperty,
            MeleeLightWeaponDamageProperty = templateItemConfiguration.MeleeLightWeaponDamageProperty,
            MeleeMediumWeaponDamageProperty = templateItemConfiguration.MeleeMediumWeaponDamageProperty,
            MeleeHeavyWeaponDamageProperty = templateItemConfiguration.MeleeHeavyWeaponDamageProperty,
            RangedLightWeaponHitProperty = templateItemConfiguration.RangedLightWeaponHitProperty,
            RangedMediumWeaponHitProperty = templateItemConfiguration.RangedMediumWeaponHitProperty,
            RangedHeavyWeaponHitProperty = templateItemConfiguration.RangedHeavyWeaponHitProperty,
            RangedLightWeaponDamageProperty = templateItemConfiguration.RangedLightWeaponDamageProperty,
            RangedMediumWeaponDamageProperty = templateItemConfiguration.RangedMediumWeaponDamageProperty,
            RangedHeavyWeaponDamageProperty = templateItemConfiguration.RangedHeavyWeaponDamageProperty,
        };
    }

    public Property? BasicAttackTargetSecondVitality { get; set; }
}