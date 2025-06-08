using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Models
{
    public class SpecificSkillModel
    {
        public SpecificSkillModel()
        {

        }
        public Guid Id { get; set; }
        public Guid SpecificSkillTemplateId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public SpecificSkillModel(SpecificSkill skill)
        {
            Id = skill.Id;
            SpecificSkillTemplateId = skill.SpecificSkillTemplateId;
            Name = skill.Name;
            Points = skill.Points;
            AttributeId = skill.AttributeId;
            SkillId = skill.SkillId;
        }

        public Guid? SkillId { get; set; }

        public Guid? AttributeId { get; set; }
    }
}