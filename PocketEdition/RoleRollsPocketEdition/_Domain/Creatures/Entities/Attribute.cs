using RoleRollsPocketEdition.Application.Creatures.Models;
using RoleRollsPocketEdition.Core;
using RoleRollsPocketEdition.Domain.CreatureTemplates.Entities;

namespace RoleRollsPocketEdition.Domain.Creatures.Entities
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