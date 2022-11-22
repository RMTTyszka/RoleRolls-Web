using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class SkillTemplate : Entity
    {
        public SkillTemplate()
        {
            MinorSkills = new List<MinorSkillTemplate>();
        }
        public SkillTemplate(SkillTemplateModel skill)
        {
            Id = skill.Id;
            Name = skill.Name;
            AttributeId = skill.AttributeId;
            MinorSkills = skill.MinorSkills.Select(minorSkill => new MinorSkillTemplate(minorSkill)).ToList();
        }

        public string Name { get; set; }
        public Guid AttributeId { get; set; }

        public List<MinorSkillTemplate> MinorSkills { get; set; }

    }
}
