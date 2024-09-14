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

        public Attribute()
        {
        }

        public Attribute(AttributeTemplate attribute)
        {
            Id = Guid.NewGuid();
            Name = attribute.Name;
            AttributeTemplateId = attribute.Id;
        }

        public void Update(AttributeModel updatedAttribute)
        {
            Value = updatedAttribute.Value;
        }
    }     
   
}