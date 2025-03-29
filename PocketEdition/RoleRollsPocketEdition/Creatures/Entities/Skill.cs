using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Skill : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeId { get; set; }
        public Attribute Attribute { get; set; }
        public Guid SkillTemplateId { get; set; }
        public SkillTemplate SkillTemplate { get; set; }
        public List<SpecificSkill> SpecificSkills { get; set; }

        public int PointsLimit => Math.Max(3 + SpecificSkills.Count - 1, 0); 
        public int UsedPoints => SpecificSkills.Sum(minorSkill => minorSkill.Points);
        public int TotalValue => Value + PointsLimit;
        public Skill()
        {
        }

        public Skill(SkillTemplate skill, Attribute attribute)
        {
            Id = Guid.NewGuid();
            Name = skill.Name;
            AttributeId = attribute.Id;
            SkillTemplateId = skill.Id;
            SkillTemplate = skill;
            SpecificSkills = skill.SpecificSkills.Select(minorSkill => new SpecificSkill(minorSkill)).ToList();
        }

        public CreatureUpdateValidationResult Update(SkillModel updatedSkill)
        {
            Value = updatedSkill.Value;
            foreach (var minorSkill in SpecificSkills)
            {
                var updatedMinorSkill = updatedSkill.SpecificSkills.First(minorsk => minorsk.SpecificSkillTemplateId == minorSkill.SpecificSkillTemplateId);
                var totalPointAfterUpdate = UsedPoints - minorSkill.Points + updatedMinorSkill.Points;
                if (totalPointAfterUpdate > PointsLimit)
                {
                    return new CreatureUpdateValidationResult(CreatureUpdateValidation.SkillPointsGreaterThanAllowed,
                        Name);
                }

                minorSkill.Update(updatedMinorSkill.Points);
            }
            return new CreatureUpdateValidationResult(CreatureUpdateValidation.Ok, null);
        }
    }
   
}