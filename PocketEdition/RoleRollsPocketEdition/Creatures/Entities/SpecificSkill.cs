using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class SpecificSkill : Entity
    {
        public Guid SpecificSkillTemplateId { get; set; }
        public SpecificSkillTemplate SpecificSkillTemplate { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public Guid? SkillId { get; set; }
        public Skill? Skill { get; set; }
        public Guid? AttributeId { get; set; }
        public Attribute? Attribute { get; set; }

        public SpecificSkill()
        {
        }
        public SpecificSkill(SpecificSkillTemplate skill)
        {
            Id = Guid.NewGuid();
            SpecificSkillTemplateId = skill.Id;
            SpecificSkillTemplate = skill;
            Name = skill.Name;
        }


        public void Update(int points)
        {
            Points = points;
        }
    }
   
}