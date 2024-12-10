using System.ComponentModel.DataAnnotations.Schema;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities
{
    public class MinorSkill : Entity
    {
        public Guid MinorSkillTemplateId { get; set; }
        public MinorSkillTemplate MinorSkillTemplate { get; set; }
        public Guid SkillId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }

        public MinorSkill()
        {
        }
        public MinorSkill(MinorSkillTemplate skill)
        {
            Id = Guid.NewGuid();
            MinorSkillTemplateId = skill.Id;
            MinorSkillTemplate = skill;
            Name = skill.Name;
        }


        public void Update(int points)
        {
            Points = points;
        }
    }
   
}