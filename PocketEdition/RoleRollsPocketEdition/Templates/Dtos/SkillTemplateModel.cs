using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class SkillTemplateModel
    {
        public SkillTemplateModel()
        {
            SpecificSkills = new List<SpecificSkillTemplateModel>();
        }
        public SkillTemplateModel(SkillTemplate skill) : base()
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeId = skill.AttributeTemplateId;
            PointsLimit = skill.PointsLimit;
            SpecificSkills = skill.SpecificSkills.OrderBy(e => e.Name).Select(minorSkill => new SpecificSkillTemplateModel(minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid? AttributeId { get; set; }

        public List<SpecificSkillTemplateModel> SpecificSkills { get; set; }
        public int PointsLimit { get; init; }
    }
}