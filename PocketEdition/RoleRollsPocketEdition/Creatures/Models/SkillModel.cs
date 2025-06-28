using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Models
{
    public class SkillModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public int Value { get; set; }
        public Guid? AttributeId { get; set; }
        public Guid SkillTemplateId { get; set; }
        public List<SpecificSkillModel> SpecificSkills { get; set; }

        public int PointsLimit { get; init; }
        public int UsedPoints { get; init; }

        public SkillModel()
        {
        }

        public SkillModel(Skill skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeId = skill.AttributeId;
            SkillTemplateId = skill.SkillTemplateId;
            SpecificSkills = skill.SpecificSkills.Select(minorSkill => new SpecificSkillModel(minorSkill)).ToList();
            PointsLimit = skill.PointsLimit;
            UsedPoints = skill.UsedPoints;
        }
    }
}