using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Attribute : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid CreatureId { get; set; }
        public Creature Creature { get; set; }
        public Guid AttributeTemplateId { get; set; }
        public AttributeTemplate AttributeTemplate { get; set; }
        public List<Skill> Skills { get; set; }
        public List<SpecificSkill> SpecificSkills { get; set; }

        // TODO bonus
        public int TotalValue => Value;
        public Attribute()
        {
        }

        public Attribute(AttributeTemplate attribute)
        {
            Id = Guid.NewGuid();
            Name = attribute.Name;
            AttributeTemplateId = attribute.Id;
            AttributeTemplate = attribute;
            Skills = attribute.SkillTemplates.Select(skill => new Skill(skill, this)).ToList();
        }

        public void Update(AttributeModel updatedAttribute)
        {
            Value = updatedAttribute.Value;
        }
    }     
   
}