using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Core;
using Attribute = RoleRollsPocketEdition._Domain.Creatures.Entities.Attribute;

namespace RoleRollsPocketEdition._Domain.CreatureTemplates.Entities
{
    public class AttributeTemplate : Entity
    {
        public AttributeTemplate()
        {

        }

        public AttributeTemplate(AttributeTemplateModel attribute)
        {
            Id = attribute.Id;
            Name = attribute.Name;
        }

        public string Name { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public ICollection<SkillTemplate> SkillTemplates { get; set; }

        public void Update(AttributeTemplateModel attributeModel)
        {
            Name = attributeModel.Name;
        }
    }
}
