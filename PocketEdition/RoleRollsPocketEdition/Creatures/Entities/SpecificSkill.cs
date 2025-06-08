using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class SpecificSkill : Entity
    {
        public Guid SpecificSkillTemplateId { get; set; }
        public SpecificSkillTemplate SpecificSkillTemplate { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public Guid? SkillId { get; set; }
        public Skill? Skill { get; set; }
        public Guid? AttributeId { get; set; }
        public Attribute? Attribute { get; set; }

        public SpecificSkill()
        {
        }
        public SpecificSkill(SpecificSkillTemplate skillTemplate, Guid skillId, List<Attribute> attributes)
        {
            Id = Guid.NewGuid();
            SpecificSkillTemplateId = skillTemplate.Id;
            SpecificSkillTemplate = skillTemplate;
            Name = skillTemplate.Name;
            SkillId = skillId;
            Skill = null;
            AttributeId = attributes.FirstOrDefault(a => a.AttributeTemplateId == skillTemplate.AttributeTemplateId)?.Id;
            Attribute = null;
        }


        public void Update(int points)
        {
            Points = points;
        }
    }
   
}