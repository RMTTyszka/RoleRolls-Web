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
            BasicAttackTargetSecondVitality = templateItemConfiguration.BasicAttackTargetSecondVitality;
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
            BlockProperty = templateItemConfiguration.BlockProperty;
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
    public Property? BasicAttackTargetFirstVitality {get; set; }
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
    public Property? BlockProperty { get; set; }

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
        BasicAttackTargetSecondVitality = model.BasicAttackTargetSecondVitality;
        BlockProperty = model.BlockProperty;
    }

    public Property? GetWeaponHitProperty(WeaponCategory weaponCategory)
    {
        return weaponCategory switch
        {
            WeaponCategory.Light => MeleeLightWeaponHitProperty,
            WeaponCategory.Medium => MeleeMediumWeaponHitProperty,
            WeaponCategory.Heavy => MeleeHeavyWeaponHitProperty,
            WeaponCategory.None => MeleeLightWeaponHitProperty,
            WeaponCategory.LightShield => MeleeLightWeaponHitProperty,
            WeaponCategory.MediumShield => MeleeMediumWeaponHitProperty,
            WeaponCategory.HeavyShield => MeleeHeavyWeaponHitProperty,
            _ => throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null)
        };
    }    
    public Property? GetWeaponDamageProperty(WeaponCategory weaponCategory)
    {
        switch (weaponCategory)
        {
            case WeaponCategory.Light:
                return MeleeLightWeaponDamageProperty;
            case WeaponCategory.Medium:
                return MeleeMediumWeaponDamageProperty;
            case WeaponCategory.Heavy:
                return MeleeHeavyWeaponDamageProperty;
            default:
                throw new ArgumentOutOfRangeException(nameof(weaponCategory), weaponCategory, null);
        }
    }
}