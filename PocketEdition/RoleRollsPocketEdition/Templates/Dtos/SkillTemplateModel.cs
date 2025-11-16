using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class SkillTemplateModel
    {
        public SkillTemplateModel()
        {
            SpecificSkillTemplates = new List<SpecificSkillTemplateModel>();
        }

        public SkillTemplateModel(SkillTemplate skill) : base()
        {
            Id = skill.Id;
            Name = skill.Name;
            PointsLimit = skill.PointsLimit;
            SpecificSkillTemplates = skill.SpecificSkillTemplates.OrderBy(e => e.Name)
                .Select(minorSkill => new SpecificSkillTemplateModel(minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }

        public List<SpecificSkillTemplateModel> SpecificSkillTemplates { get; set; }
        public int PointsLimit { get; init; }
    }
}


