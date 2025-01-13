namespace RoleRollsPocketEdition.Itens.Configurations;

public class ItemConfigurationModel
{
    public Guid? ArmorDefenseId { get; set; }
    public Guid? LightWeaponHitPropertyId { get; set; }
    public Guid? MediumWeaponHitPropertyId { get; set; }
    public Guid? HeavyWeaponHitPropertyId { get; set; }   
    public Guid? LightWeaponDamagePropertyId { get; set; }
    public Guid? MediumWeaponDamagePropertyId { get; set; }
    public Guid? HeavyWeaponDamagePropertyId { get; set; }
    public Guid? BasicAttackTargetLifeId { get; set; }

    public static ItemConfigurationModel FromConfiguration(ItemConfiguration? itemConfiguration)
    {
        if (itemConfiguration == null)
        {
            return new ItemConfigurationModel();
        }

        return new ItemConfigurationModel
        {
            ArmorDefenseId = itemConfiguration.ArmorDefenseId,
            LightWeaponHitPropertyId = itemConfiguration.LightWeaponHitPropertyId,
            MediumWeaponHitPropertyId = itemConfiguration.MediumWeaponHitPropertyId,
            HeavyWeaponHitPropertyId = itemConfiguration.HeavyWeaponHitPropertyId,
            LightWeaponDamagePropertyId = itemConfiguration.LightWeaponDamagePropertyId,
            MediumWeaponDamagePropertyId = itemConfiguration.MediumWeaponDamagePropertyId,
            HeavyWeaponDamagePropertyId = itemConfiguration.HeavyWeaponDamagePropertyId,
            BasicAttackTargetLifeId = itemConfiguration.BasicAttackTargetLifeId,
        };
    }
}