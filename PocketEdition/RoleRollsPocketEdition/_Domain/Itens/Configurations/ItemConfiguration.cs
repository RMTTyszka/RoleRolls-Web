using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Itens.Configurations;

public class ItemConfiguration : Entity
{
    public Campaign Campaign { get; set; }
    [ForeignKey(nameof(Campaign))]
    public Guid CampaignId { get; set; }
    public Guid? ArmorDefenseId { get; private set; }
    public Guid? BasicAttackTargetLifeId { get; private set; }
    public Guid? LightWeaponHitPropertyId { get; private set; }
    public Guid? MediumWeaponHitPropertyId { get; private set; }
    public Guid? HeavyWeaponHitPropertyId { get; private set; }   
    public Guid? LightWeaponDamagePropertyId { get; private set; }
    public Guid? MediumWeaponDamagePropertyId { get; private set; }
    public Guid? HeavyWeaponDamagePropertyId { get; private set; }

    public void Update(ItemConfigurationModel model)
    {
        ArmorDefenseId = model.ArmorDefenseId;
        LightWeaponHitPropertyId = model.LightWeaponHitPropertyId;
        MediumWeaponHitPropertyId = model.MediumWeaponHitPropertyId;
        HeavyWeaponHitPropertyId = model.HeavyWeaponHitPropertyId;
        LightWeaponDamagePropertyId = model.LightWeaponDamagePropertyId;
        MediumWeaponDamagePropertyId = model.MediumWeaponDamagePropertyId;
        HeavyWeaponDamagePropertyId = model.HeavyWeaponDamagePropertyId;
        BasicAttackTargetLifeId = model.BasicAttackTargetLifeId;
    }

    public Guid GetWeaponHitProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return LightWeaponHitPropertyId.Value;
            case WeaponCategory.Medium:
                return MediumWeaponHitPropertyId.Value;
            case WeaponCategory.Heavy:
                return HeavyWeaponHitPropertyId.Value;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }    
    public Guid GetWeaponDamageProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return LightWeaponDamagePropertyId.Value;
            case WeaponCategory.Medium:
                return MediumWeaponDamagePropertyId.Value;
            case WeaponCategory.Heavy:
                return HeavyWeaponDamagePropertyId.Value;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }
}