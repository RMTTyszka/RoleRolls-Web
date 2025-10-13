using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Skill : Entity
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Guid SkillTemplateId { get; set; }
        public SkillTemplate SkillTemplate { get; set; }
        public List<SpecificSkill> SpecificSkills { get; set; }

        public int PointsLimit => Math.Max(3 + SpecificSkills.Count - 1, 0);
        public int UsedPoints => SpecificSkills.Sum(minorSkill => minorSkill.Points);

        public Skill()
        {
        }

        public Skill(SkillTemplate skill)
        {
            Id = Guid.NewGuid();
            Name = skill.Name;
            SkillTemplateId = skill.Id;
            SkillTemplate = skill;
            SpecificSkills = skill.SpecificSkillTemplates.Select(minorSkill => new SpecificSkill(minorSkill, this, []))
                .ToList();
        }

        public Skill(SkillTemplate skillTemplate, List<Attribute> attributes)
        {
            var attribute = attributes.FirstOrDefault(a => a.AttributeTemplateId == skillTemplate.AttributeTemplateId);
                
            Id = Guid.NewGuid();
            Name = skillTemplate.Name;
            SkillTemplateId = skillTemplate.Id;
            SkillTemplate = skillTemplate;
            SpecificSkills = skillTemplate.SpecificSkillTemplates
                .Select(minorSkill => new SpecificSkill(minorSkill, this, attributes)).ToList();
        }

        public CreatureUpdateValidationResult Update(SkillModel updatedSkill)
        {
            Points = updatedSkill.Value;
            foreach (var minorSkill in SpecificSkills)
            {
                var updatedMinorSkill = updatedSkill.SpecificSkills.First(minorsk =>
                    minorsk.SpecificSkillTemplateId == minorSkill.SpecificSkillTemplateId);
                minorSkill.Update(updatedMinorSkill.Points);
            }

            return new CreatureUpdateValidationResult(CreatureUpdateValidation.Ok, null);
        }
    }
}
