using RoleRollsPocketEdition._Application.Creatures.Models;
using RoleRollsPocketEdition._Domain.CreatureTemplates.Entities;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.Creatures.Entities
{
    public class Attribute : Entity
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Guid AttributeTemplateId { get; set; }
        public Guid CreatureId { get; set; }
        public Creature Creature { get; set; }
        public AttributeTemplate AttributeTemplate { get; set; }
        public List<Skill> Skills { get; set; }

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
        }

        public void Update(AttributeModel updatedAttribute)
        {
            Value = updatedAttribute.Value;
        }
    }     
   
}