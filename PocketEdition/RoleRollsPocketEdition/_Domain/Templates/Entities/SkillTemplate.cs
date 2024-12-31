using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition._Domain.Creatures.Entities;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition._Domain.CreatureTemplates.Entities
{
    public class SkillTemplate : Entity
    {
        public SkillTemplate()
        {
            MinorSkills = new List<MinorSkillTemplate>();
        }
        public SkillTemplate(Guid attributeTemplateId, SkillTemplateModel skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeTemplateId = attributeTemplateId;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkillTemplate(skill.Id, minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid AttributeTemplateId { get; set; }
        public AttributeTemplate AttributeTemplate { get; set; }

        public ICollection<Skill> Skills { get; set; }
        public List<MinorSkillTemplate> MinorSkills { get; set; }
        
        public int PointsLimit => MinorSkills.Count * 3 - MinorSkills.Count - 1; 

        public void Update(SkillTemplateModel skillModel)
        {
            Name = skillModel.Name;
        }        
        public async Task AddMinorSkillAsync(MinorSkillTemplate minorSkill, RoleRollsDbContext dbContext)
        {
            MinorSkills.Add(minorSkill);
            await dbContext.MinorSkillTemplates.AddAsync(minorSkill);
        }        

    }
}
