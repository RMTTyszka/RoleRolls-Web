using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Models;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Creatures.Entities
{
    public class Attribute : Entity
    {
        public string Name { get; set; }
        public int Points { get; set; }
        public Guid CreatureId { get; set; }
        public Creature Creature { get; set; }
        public Guid AttributeTemplateId { get; set; }
        public AttributeTemplate AttributeTemplate { get; set; }
        public List<SpecificSkill> SpecificSkills { get; set; }

        // TODO bonus
        public int TotalValue => Points;
        public Attribute()
        {
        }

        public Attribute(AttributeTemplate attribute)
        {
            Id = Guid.NewGuid();
            Name = attribute.Name;
            AttributeTemplateId = attribute.Id;
            AttributeTemplate = attribute;
        }

        public void Update(AttributeModel updatedAttribute)
        {
            Points = updatedAttribute.Value;
        }
    }     
   
}
