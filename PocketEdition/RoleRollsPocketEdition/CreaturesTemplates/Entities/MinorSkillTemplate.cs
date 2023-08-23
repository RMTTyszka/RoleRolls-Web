using RoleRollsPocketEdition.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Global;
using RoleRollsPocketEdition.Infrastructure;

namespace RoleRollsPocketEdition.CreaturesTemplates.Entities
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

        public void Update(MinorSkillTemplateModel minorSkillModel, RoleRollsDbContext dbContext)
        {
            Name = minorSkillModel.Name;
            dbContext.MinorSkillTemplates.Update(this);
        }
    }
}
