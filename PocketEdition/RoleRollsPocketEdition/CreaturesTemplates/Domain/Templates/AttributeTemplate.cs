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
            attribute.Id = attribute.Id;
            attribute.Name = attribute.Name;
        }

        public string Name { get; set; }
    }
}
