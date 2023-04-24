using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.CreaturesTemplates.Domain.Templates
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
