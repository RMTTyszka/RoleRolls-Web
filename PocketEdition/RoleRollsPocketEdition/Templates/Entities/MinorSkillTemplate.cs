using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Infrastructure;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class MinorSkillTemplate : Entity
    {
        public MinorSkillTemplate()
        {

        }
        public MinorSkillTemplate(Guid skillId, MinorSkillTemplateModel minorSkill)
        {
            Id = minorSkill.Id;
            SkillTemplateId = skillId;
            Name = minorSkill.Name;
        }

        public string Name { get; set; }
        public Guid SkillTemplateId { get; set; }
        public SkillTemplate SkillTemplate { get; set; }

        public ICollection<MinorSkill> MinorSkills { get; set; }
        public void Update(MinorSkillTemplateModel minorSkillModel, RoleRollsDbContext dbContext)
        {
            Name = minorSkillModel.Name;
            dbContext.MinorSkillTemplates.Update(this);
        }
    }
}
