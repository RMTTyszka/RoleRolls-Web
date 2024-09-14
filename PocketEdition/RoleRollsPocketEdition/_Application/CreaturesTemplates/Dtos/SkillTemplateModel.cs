using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;

namespace RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos
{
    public class SkillTemplateModel
    {
        public SkillTemplateModel()
        {
            MinorSkills = new List<MinorSkillTemplateModel>();
        }
        public SkillTemplateModel(SkillTemplate skill) : base()
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeId = skill.AttributeId;
            PointsLimit = skill.PointsLimit;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkillTemplateModel(minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }

        public List<MinorSkillTemplateModel> MinorSkills { get; set; }
        public int PointsLimit { get; init; }
    }
}