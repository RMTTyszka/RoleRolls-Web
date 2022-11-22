using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos
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
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkillTemplateModel(minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid Id { get; set; }
        public Guid AttributeId { get; set; }

        public List<MinorSkillTemplateModel> MinorSkills { get; set; }
    }
}