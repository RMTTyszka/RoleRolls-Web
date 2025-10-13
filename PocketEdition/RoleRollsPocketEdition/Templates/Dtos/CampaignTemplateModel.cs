using RoleRollsPocketEdition.Archetypes.Models;
using RoleRollsPocketEdition.CreatureTypes.Models;
using RoleRollsPocketEdition.Itens.Configurations;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class CampaignTemplateModel
    {

        public CampaignTemplateModel()
        {

        }
        public CampaignTemplateModel(CampaignTemplate template)
        {
            Id = template.Id;
            Name = template.Name;
            CreatureTypeTitle = template.CreatureTypeTitle;
            ArchetypeTitle = template.ArchetypeTitle;
            TotalAttributePoints = template.TotalAttributePoints;
            MaxAttributePoints = template.MaxAttributePoints;
            TotalSkillsPoints = template.TotalSkillsPoints;
            Default = template.Default;
            Attributes = template.Attributes.OrderBy(e => e.Name).Select(attribute => new AttributeTemplateModel(attribute)).ToList();
            Skills = template.Skills.OrderBy(e => e.Name).Select(skill => new SkillTemplateModel(skill)).ToList();
            Vitalities = template.Vitalities.OrderBy(e => e.Name).Select(vitality => new VitalityTemplateModel(vitality)).ToList();
            Defenses = template.Defenses.OrderBy(e => e.Name).Select(defense => new DefenseTemplateModel(defense)).ToList();
            CreatureTypes = template.CreatureTypes.OrderBy(e => e.Name).Select(defense => new CreatureTypeModel(defense)).ToList();
            Archetypes = template.Archetypes.OrderBy(e => e.Name).Select(defense => new ArchetypeModel(defense)).ToList();
            ItemConfiguration = ItemConfigurationModel.FromConfiguration(template.ItemConfiguration);
        }

        public string? ArchetypeTitle { get; set; }

        public string? CreatureTypeTitle { get; set; }

        public List<CreatureTypeModel> CreatureTypes { get; set; } = [];


        public bool Default { get; set; }

        public List<DefenseTemplateModel> Defenses { get; set; } = [];
        public List<ArchetypeModel> Archetypes { get; set; } = [];

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int MaxAttributePoints { get; init; }
        public int TotalAttributePoints { get; set; }
        // 4
        public int TotalSkillsPoints { get; set; }
        public virtual ICollection<AttributeTemplateModel> Attributes { get; set; }
        public ICollection<SkillTemplateModel> Skills { get; set; }

        public ICollection<VitalityTemplateModel> Vitalities { get; set; }
        public ItemConfigurationModel ItemConfiguration { get; set; }

        public static CampaignTemplateModel? FromTemplate(CampaignTemplate? campaignCreatureTemplate)
        {
            if (campaignCreatureTemplate == null)
            {
                return new CampaignTemplateModel();
            }
            return new CampaignTemplateModel(campaignCreatureTemplate);
        }
    }
}
