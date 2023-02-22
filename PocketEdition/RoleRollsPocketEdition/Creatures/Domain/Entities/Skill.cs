using RoleRollsPocketEdition.Creatures.Domain.Models;

namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class Skill : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeId { get; set; }
        public Guid SkillTemplateId { get; set; }
        public List<MinorSkill> MinorSkills { get; set; }
        public Skill()
        {
        }

        public Skill(SkillTemplate skill)
        {
            Id = Guid.NewGuid();
            Name = skill.Name;
            AttributeId = skill.AttributeId;
            SkillTemplateId = skill.Id;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkill(minorSkill)).ToList();
        }

        internal void Update(SkillModel updatedSkill)
        {
            Value = updatedSkill.Value;
            foreach (var minorSkill in MinorSkills)
            {
                var updatedMinorSkill = updatedSkill.MinorSkills.First(minorsk => minorsk.MinorSkillTemplateId == minorSkill.MinorSkillTemplateId);
                minorSkill.Update(updatedMinorSkill);
            }
        }
    }
   
}