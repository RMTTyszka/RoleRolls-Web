using RoleRollsPocketEdition.CreaturesTemplates.Entities;

namespace RoleRollsPocketEdition.CreaturesTemplates.Dtos
{
    public class AttributeTemplateModel
    {
        public AttributeTemplateModel()
        {

        }
        public AttributeTemplateModel(AttributeTemplate attribute)
        {
            Id = attribute.Id;
            Name = attribute.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}