using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;
using Attribute = RoleRollsPocketEdition.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class AttributeTemplate : Entity
    {
        public AttributeTemplate()
        {
        }

        public AttributeTemplate(AttributeTemplateModel attribute)
        {
            Id = attribute.Id;
            Name = attribute.Name;
        }

        public string Name { get; set; }
        public Guid CampaignTemplateId { get; set; }
        public CampaignTemplate CampaignTemplate { get; set; }
        public ICollection<Attribute> Attributes { get; set; } = [];
        public ICollection<SpecificSkillTemplate> SpecificSkillTemplates { get; set; } = [];
        public ICollection<SkillTemplate> SkillTemplates { get; set; } = [];

        public void Update(AttributeTemplateModel attributeModel)
        {
            Name = attributeModel.Name;
        }

        public SkillTemplate AddSkill(SkillTemplateModel skillModel)
        {
            var skill = new SkillTemplate(this, skillModel);
            SkillTemplates.Add(skill);
            return skill;
        }
    }
}