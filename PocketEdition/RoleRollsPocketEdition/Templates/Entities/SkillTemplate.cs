using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class SkillTemplate : Entity
    {
        public SkillTemplate()
        {
        }

        public SkillTemplate(AttributeTemplate? attributeTemplate, SkillTemplateModel skill) : base()
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeTemplateId = attributeTemplate?.Id;
            AttributeTemplate = attributeTemplate;
            SpecificSkillTemplates = skill.SpecificSkillTemplates
                .Select(minorSkill => new SpecificSkillTemplate(skill.Id, minorSkill)).ToList();
        }

        public string Name { get; set; } = string.Empty;
        public Guid CampaignTemplateId { get; set; }
        public CampaignTemplate? CampaignTemplate { get; set; }
        public Guid? AttributeTemplateId { get; set; }
        public AttributeTemplate? AttributeTemplate { get; set; }

        public ICollection<Skill> Skills { get; set; } = [];
        public List<SpecificSkillTemplate> SpecificSkillTemplates { get; set; } = [];

        public int PointsLimit => SpecificSkillTemplates.Count * 3 - SpecificSkillTemplates.Count - 1;

        public void Update(SkillTemplateModel skillModel)
        {
            Name = skillModel.Name;
        }

        public async Task AddMinorSkillAsync(SpecificSkillTemplate specificSkill, RoleRollsDbContext dbContext)
        {
            SpecificSkillTemplates.Add(specificSkill);
            await dbContext.MinorSkillTemplates.AddAsync(specificSkill);
        }
    }
}
