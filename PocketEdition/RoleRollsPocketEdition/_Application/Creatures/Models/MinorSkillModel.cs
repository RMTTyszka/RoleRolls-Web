using RoleRollsPocketEdition._Domain.Creatures.Entities;

namespace RoleRollsPocketEdition._Application.Creatures.Models
{
    public class MinorSkillModel
    {
        public MinorSkillModel()
        {

        }
        public Guid Id { get; set; }
        public Guid MinorSkillTemplateId { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public MinorSkillModel(MinorSkill skill)
        {
            Id = skill.Id;
            MinorSkillTemplateId = skill.MinorSkillTemplateId;
            Name = skill.Name;
            Points = skill.Points;
        }
    }
}