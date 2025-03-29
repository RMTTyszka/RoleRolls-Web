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
            SpecificSkills = new List<SpecificSkillTemplate>();
        }
        public SkillTemplate(Guid? attributeTemplateId, SkillTemplateModel skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeTemplateId = attributeTemplateId;
            SpecificSkills = skill.SpecificSkills.Select(minorSkill => new SpecificSkillTemplate(skill.Id, minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid? AttributeTemplateId { get; set; }
        public AttributeTemplate? AttributeTemplate { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public List<SpecificSkillTemplate> SpecificSkills { get; set; }
        
        public int PointsLimit => SpecificSkills.Count * 3 - SpecificSkills.Count - 1; 

        public void Update(SkillTemplateModel skillModel)
        {
            Name = skillModel.Name;
        }        
        public async Task AddMinorSkillAsync(SpecificSkillTemplate specificSkill, RoleRollsDbContext dbContext)
        {
            SpecificSkills.Add(specificSkill);
            await dbContext.MinorSkillTemplates.AddAsync(specificSkill);
        }        

    }
}
