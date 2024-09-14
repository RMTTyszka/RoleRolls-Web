using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
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
        public SkillTemplate(Guid attributeId, SkillTemplateModel skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeId = attributeId;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkillTemplate(skill.Id, minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid AttributeId { get; set; }

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
