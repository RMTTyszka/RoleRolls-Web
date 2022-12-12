using RoleRollsPocketEdition.Creatures.Domain.Models;

namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class MinorSkill : Entity
    {
        public Guid MinorSkillTemplateId { get; set; }
        public string Name { get; set; }
        public SkillProficience SkillProficience { get; set; }

        public MinorSkill()
        {
        }
        public MinorSkill(MinorSkillTemplate skill)
        {
            Id = Guid.NewGuid();
            MinorSkillTemplateId = skill.Id;
            Name = skill.Name;
        }

        internal void Update(MinorSkillModel updatedMinorSkill)
        {
            SkillProficience = updatedMinorSkill.SkillProficience;
        }
    }
   
}