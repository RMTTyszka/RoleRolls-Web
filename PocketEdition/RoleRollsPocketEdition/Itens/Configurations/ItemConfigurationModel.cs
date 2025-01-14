namespace RoleRollsPocketEdition.Itens.Configurations;

public class ItemConfigurationModel
{
    public Guid? ArmorPropertyId { get; set; }

    public Guid? MeleeLightWeaponHitPropertyId { get; set; }
    public Guid? MeleeMediumWeaponHitPropertyId { get; set; }
    public Guid? MeleeHeavyWeaponHitPropertyId { get; set; }   
    public Guid? MeleeLightWeaponDamagePropertyId { get; set; }
    public Guid? MeleeMediumWeaponDamagePropertyId { get; set; }
    public Guid? MeleeHeavyWeaponDamagePropertyId { get; set; }  
    public Guid? RangedLightWeaponHitPropertyId { get; set; }
    public Guid? RangedMediumWeaponHitPropertyId { get; set; }
    public Guid? RangedHeavyWeaponHitPropertyId { get; set; }   
    public Guid? RangedLightWeaponDamagePropertyId { get; set; }
    public Guid? RangedMediumWeaponDamagePropertyId { get; set; }
    public Guid? RangedHeavyWeaponDamagePropertyId { get; set; }
    public Guid? BasicAttackTargetFirstLifeId { get; set; }

    public static ItemConfigurationModel FromConfiguration(ItemConfiguration? templateItemConfiguration)
    {
        if (templateItemConfiguration == null)
        {
            return new ItemConfigurationModel();
        }

        return new ItemConfigurationModel
        {
            ArmorPropertyId = templateItemConfiguration.ArmorPropertyId,
            BasicAttackTargetFirstLifeId = templateItemConfiguration.BasicAttackTargetFirstLifeId,
            BasicAttackTargetSecondLifeId = templateItemConfiguration.BasicAttackTargetSecondLifeId,
            MeleeLightWeaponHitPropertyId = templateItemConfiguration.MeleeLightWeaponHitPropertyId,
            MeleeMediumWeaponHitPropertyId = templateItemConfiguration.MeleeMediumWeaponHitPropertyId,
            MeleeHeavyWeaponHitPropertyId = templateItemConfiguration.MeleeHeavyWeaponHitPropertyId,
            MeleeLightWeaponDamagePropertyId = templateItemConfiguration.MeleeLightWeaponDamagePropertyId,
            MeleeMediumWeaponDamagePropertyId = templateItemConfiguration.MeleeMediumWeaponDamagePropertyId,
            MeleeHeavyWeaponDamagePropertyId = templateItemConfiguration.MeleeHeavyWeaponDamagePropertyId,
            RangedLightWeaponHitPropertyId = templateItemConfiguration.RangedLightWeaponHitPropertyId,
            RangedMediumWeaponHitPropertyId = templateItemConfiguration.RangedMediumWeaponHitPropertyId,
            RangedHeavyWeaponHitPropertyId = templateItemConfiguration.RangedHeavyWeaponHitPropertyId,
            RangedLightWeaponDamagePropertyId = templateItemConfiguration.RangedLightWeaponDamagePropertyId,
            RangedMediumWeaponDamagePropertyId = templateItemConfiguration.RangedMediumWeaponDamagePropertyId,
            RangedHeavyWeaponDamagePropertyId = templateItemConfiguration.RangedHeavyWeaponDamagePropertyId,
        };
    }

    public Guid? BasicAttackTargetSecondLifeId { get; set; }
}