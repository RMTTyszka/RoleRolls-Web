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
            ArmorProperty = templateItemConfiguration.ArmorProperty;
            BasicAttackTargetFirstVitality = templateItemConfiguration.BasicAttackTargetFirstVitality;
            MeleeLightWeaponHitProperty = templateItemConfiguration.MeleeLightWeaponHitProperty;
            MeleeMediumWeaponHitProperty = templateItemConfiguration.MeleeMediumWeaponHitProperty;
            MeleeHeavyWeaponHitProperty = templateItemConfiguration.MeleeHeavyWeaponHitProperty;
            MeleeLightWeaponDamageProperty = templateItemConfiguration.MeleeLightWeaponDamageProperty;
            MeleeMediumWeaponDamageProperty = templateItemConfiguration.MeleeMediumWeaponDamageProperty;
            MeleeHeavyWeaponDamageProperty = templateItemConfiguration.MeleeHeavyWeaponDamageProperty;    
            RangedLightWeaponHitProperty = templateItemConfiguration.RangedLightWeaponHitProperty;
            RangedMediumWeaponHitProperty = templateItemConfiguration.RangedMediumWeaponHitProperty;
            RangedHeavyWeaponHitProperty = templateItemConfiguration.RangedHeavyWeaponHitProperty;
            RangedLightWeaponDamageProperty = templateItemConfiguration.RangedLightWeaponDamageProperty;
            RangedMediumWeaponDamageProperty = templateItemConfiguration.RangedMediumWeaponDamageProperty;
            RangedHeavyWeaponDamageProperty = templateItemConfiguration.RangedHeavyWeaponDamageProperty;
        }
    }

    public ItemConfiguration(CampaignTemplate campaignTemplate)
    {
        CampaignTemplate = campaignTemplate;
        CampaignTemplateId = campaignTemplate.Id;
    }

    public CampaignTemplate CampaignTemplate { get; set; }
    public Guid CampaignTemplateId { get; set; }
    public Property? ArmorProperty { get; set; }
    public Property? BasicAttackTargetFirstVitality {get; private set; }
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
    public Property? BasicAttackTargetSecondVitality { get; set; }

    public void Update(ItemConfigurationModel model)
    {
        ArmorProperty = model.ArmorProperty;
        MeleeLightWeaponHitProperty = model.MeleeLightWeaponHitProperty;
        MeleeMediumWeaponHitProperty = model.MeleeMediumWeaponHitProperty;
        MeleeHeavyWeaponHitProperty = model.MeleeHeavyWeaponHitProperty;
        MeleeLightWeaponDamageProperty = model.MeleeLightWeaponDamageProperty;
        MeleeMediumWeaponDamageProperty = model.MeleeMediumWeaponDamageProperty;
        MeleeHeavyWeaponDamageProperty = model.MeleeHeavyWeaponDamageProperty;       
        RangedLightWeaponHitProperty = model.RangedLightWeaponHitProperty;
        RangedMediumWeaponHitProperty = model.RangedMediumWeaponHitProperty;
        RangedHeavyWeaponHitProperty = model.RangedHeavyWeaponHitProperty;
        RangedLightWeaponDamageProperty = model.RangedLightWeaponDamageProperty;
        RangedMediumWeaponDamageProperty = model.RangedMediumWeaponDamageProperty;
        RangedHeavyWeaponDamageProperty = model.RangedHeavyWeaponDamageProperty;
        BasicAttackTargetFirstVitality = model.BasicAttackTargetFirstVitality;
    }

    public Guid GetWeaponHitProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return MeleeLightWeaponHitProperty.PropertyId;
            case WeaponCategory.Medium:
                return MeleeMediumWeaponHitProperty.PropertyId;
            case WeaponCategory.Heavy:
                return MeleeHeavyWeaponHitProperty.PropertyId;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }    
    public Guid GetWeaponDamageProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return MeleeLightWeaponDamageProperty.PropertyId;
            case WeaponCategory.Medium:
                return MeleeMediumWeaponDamageProperty.PropertyId;
            case WeaponCategory.Heavy:
                return MeleeHeavyWeaponDamageProperty.PropertyId;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }
}