using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Itens.Configurations;

public class ItemConfiguration : Entity
{
    public Campaign Campaign { get; set; }
    public Guid CampaignId { get; set; }
    public Guid? ArmorDefenseId { get; set; }
    public Guid? LightWeaponHitPropertyId { get; set; }
    public Guid? MediumWeaponHitPropertyId { get; set; }
    public Guid? HeavyWeaponHitPropertyId { get; set; }   
    public Guid? LightWeaponDamagePropertyId { get; set; }
    public Guid? MediumWeaponDamagePropertyId { get; set; }
    public Guid? HeavyWeaponDamagePropertyId { get; set; }

    public void Update(ItemConfigurationModel model)
    {
        ArmorDefenseId = model.ArmorDefenseId;
        LightWeaponHitPropertyId = model.LightWeaponHitPropertyId;
        MediumWeaponHitPropertyId = model.MediumWeaponHitPropertyId;
        HeavyWeaponHitPropertyId = model.HeavyWeaponHitPropertyId;
        LightWeaponDamagePropertyId = model.LightWeaponDamagePropertyId;
        MediumWeaponDamagePropertyId = model.MediumWeaponDamagePropertyId;
        HeavyWeaponDamagePropertyId = model.HeavyWeaponDamagePropertyId;
    }

    public Guid? GetWeaponHitProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return LightWeaponHitPropertyId;
            case WeaponCategory.Medium:
                return MediumWeaponHitPropertyId;
            case WeaponCategory.Heavy:
                return HeavyWeaponHitPropertyId;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }
}