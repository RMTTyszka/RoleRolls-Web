using RoleRollsPocketEdition._Domain.Campaigns.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Itens.Configurations;

public class ItemConfiguration : Entity
{
    public Campaign Campaign { get; set; }
    public Guid CampaignId { get; set; }
    public Guid? ArmorDefenseId { get; set; }
    public Guid? LightWeaponHitAttributeId { get; set; }
    public Guid? MediumWeaponHitAttributeId { get; set; }
    public Guid? HeavyWeaponHitAttributeId { get; set; }   
    public Guid? LightWeaponDamageAttributeId { get; set; }
    public Guid? MediumWeaponDamageAttributeId { get; set; }
    public Guid? HeavyWeaponDamageAttributeId { get; set; }

    public void Update(ItemConfigurationModel model)
    {
        ArmorDefenseId = model.ArmorDefenseId;
        LightWeaponHitAttributeId = model.LightWeaponHitAttributeId;
        MediumWeaponHitAttributeId = model.MediumWeaponHitAttributeId;
        HeavyWeaponHitAttributeId = model.HeavyWeaponHitAttributeId;
        LightWeaponDamageAttributeId = model.LightWeaponDamageAttributeId;
        MediumWeaponDamageAttributeId = model.MediumWeaponDamageAttributeId;
        HeavyWeaponDamageAttributeId = model.HeavyWeaponDamageAttributeId;
    }
}