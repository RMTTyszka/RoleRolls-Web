using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Itens.Configurations;

public class ItemConfiguration : Entity
{
    public ItemConfiguration()
    {
        
    }
    public ItemConfiguration(CampaignTemplate campaignTemplate, ItemConfigurationModel? templateItemConfiguration) : this(campaignTemplate)
    {
        if (templateItemConfiguration is not null)
        {
            ArmorPropertyId = templateItemConfiguration.ArmorPropertyId;
            BasicAttackTargetFirstLifeId = templateItemConfiguration.BasicAttackTargetFirstLifeId;
            MeleeLightWeaponHitPropertyId = templateItemConfiguration.MeleeLightWeaponHitPropertyId;
            MeleeMediumWeaponHitPropertyId = templateItemConfiguration.MeleeMediumWeaponHitPropertyId;
            MeleeHeavyWeaponHitPropertyId = templateItemConfiguration.MeleeHeavyWeaponHitPropertyId;
            MeleeLightWeaponDamagePropertyId = templateItemConfiguration.MeleeLightWeaponDamagePropertyId;
            MeleeMediumWeaponDamagePropertyId = templateItemConfiguration.MeleeMediumWeaponDamagePropertyId;
            MeleeHeavyWeaponDamagePropertyId = templateItemConfiguration.MeleeHeavyWeaponDamagePropertyId;    
            
            RangedLightWeaponHitPropertyId = templateItemConfiguration.RangedLightWeaponHitPropertyId;
            RangedMediumWeaponHitPropertyId = templateItemConfiguration.RangedMediumWeaponHitPropertyId;
            RangedHeavyWeaponHitPropertyId = templateItemConfiguration.RangedHeavyWeaponHitPropertyId;
            RangedLightWeaponDamagePropertyId = templateItemConfiguration.RangedLightWeaponDamagePropertyId;
            RangedMediumWeaponDamagePropertyId = templateItemConfiguration.RangedMediumWeaponDamagePropertyId;
            RangedHeavyWeaponDamagePropertyId = templateItemConfiguration.RangedHeavyWeaponDamagePropertyId;
        }
    }

    public ItemConfiguration(CampaignTemplate campaignTemplate)
    {
        CampaignTemplate = campaignTemplate;
        CampaignTemplateId = campaignTemplate.Id;
    }

    public CampaignTemplate CampaignTemplate { get; set; }
    public Guid CampaignTemplateId { get; set; }
    public Guid? ArmorPropertyId { get; set; }
    public Guid? BasicAttackTargetFirstLifeId { get; private set; }
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
    public Guid? BasicAttackTargetSecondLifeId { get; set; }

    public void Update(ItemConfigurationModel model)
    {
        ArmorPropertyId = model.ArmorPropertyId;
        MeleeLightWeaponHitPropertyId = model.MeleeLightWeaponHitPropertyId;
        MeleeMediumWeaponHitPropertyId = model.MeleeMediumWeaponHitPropertyId;
        MeleeHeavyWeaponHitPropertyId = model.MeleeHeavyWeaponHitPropertyId;
        MeleeLightWeaponDamagePropertyId = model.MeleeLightWeaponDamagePropertyId;
        MeleeMediumWeaponDamagePropertyId = model.MeleeMediumWeaponDamagePropertyId;
        MeleeHeavyWeaponDamagePropertyId = model.MeleeHeavyWeaponDamagePropertyId;       
        
        RangedLightWeaponHitPropertyId = model.RangedLightWeaponHitPropertyId;
        RangedMediumWeaponHitPropertyId = model.RangedMediumWeaponHitPropertyId;
        RangedHeavyWeaponHitPropertyId = model.RangedHeavyWeaponHitPropertyId;
        RangedLightWeaponDamagePropertyId = model.RangedLightWeaponDamagePropertyId;
        RangedMediumWeaponDamagePropertyId = model.RangedMediumWeaponDamagePropertyId;
        RangedHeavyWeaponDamagePropertyId = model.RangedHeavyWeaponDamagePropertyId;
        BasicAttackTargetFirstLifeId = model.BasicAttackTargetFirstLifeId;
    }

    public Guid GetWeaponHitProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return MeleeLightWeaponHitPropertyId.Value;
            case WeaponCategory.Medium:
                return MeleeMediumWeaponHitPropertyId.Value;
            case WeaponCategory.Heavy:
                return MeleeHeavyWeaponHitPropertyId.Value;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }    
    public Guid GetWeaponDamageProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return MeleeLightWeaponDamagePropertyId.Value;
            case WeaponCategory.Medium:
                return MeleeMediumWeaponDamagePropertyId.Value;
            case WeaponCategory.Heavy:
                return MeleeHeavyWeaponDamagePropertyId.Value;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }
}