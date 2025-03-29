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
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<SpecificSkillTemplate> SpecificSkillTemplates { get; set; }
        public ICollection<SkillTemplate> SkillTemplates { get; set; }

        public void Update(AttributeTemplateModel attributeModel)
        {
            Name = attributeModel.Name;
        }

        public async Task<SkillTemplate> AddSkill(SkillTemplateModel skillModel, RoleRollsDbContext context)
        {
            var skill = new SkillTemplate(Id, skillModel);
            SkillTemplates.Add(skill);
            await context.SkillTemplates.AddAsync(skill);
            return skill;
        }
    }
}
