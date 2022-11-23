using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Creatures.Domain
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

        internal void Update(AttributeTemplateModel attributeModel)
        {
            Name = attributeModel.Name;
        }
    }
}
