namespace RoleRollsPocketEdition._Domain.Itens.Configurations;

public class ItemConfigurationModel
{
    public Guid? ArmorDefenseId { get; set; }
    public Guid? LightWeaponHitAttributeId { get; set; }
    public Guid? MediumWeaponHitAttributeId { get; set; }
    public Guid? HeavyWeaponHitAttributeId { get; set; }   
    public Guid? LightWeaponDamageAttributeId { get; set; }
    public Guid? MediumWeaponDamageAttributeId { get; set; }
    public Guid? HeavyWeaponDamageAttributeId { get; set; }

    public static ItemConfigurationModel FromConfiguration(ItemConfiguration? itemConfiguration)
    {
        if (itemConfiguration == null)
        {
            return new ItemConfigurationModel();
        }

        return new ItemConfigurationModel
        {
            ArmorDefenseId = itemConfiguration.ArmorDefenseId,
            LightWeaponHitAttributeId = itemConfiguration.LightWeaponHitAttributeId,
            MediumWeaponHitAttributeId = itemConfiguration.MediumWeaponHitAttributeId,
            HeavyWeaponHitAttributeId = itemConfiguration.HeavyWeaponHitAttributeId,
            LightWeaponDamageAttributeId = itemConfiguration.LightWeaponDamageAttributeId,
            MediumWeaponDamageAttributeId = itemConfiguration.MediumWeaponDamageAttributeId,
            HeavyWeaponDamageAttributeId = itemConfiguration.HeavyWeaponDamageAttributeId,
        };
    }
}