using RoleRollsPocketEdition.Creatures.Domain.Models;
using RoleRollsPocketEdition.CreaturesTemplates.Domain.Templates;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Creatures.Domain.Entities
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

        internal void Update(AttributeModel updatedAttribute)
        {
            Value = updatedAttribute.Value;
        }
    }     
   
}