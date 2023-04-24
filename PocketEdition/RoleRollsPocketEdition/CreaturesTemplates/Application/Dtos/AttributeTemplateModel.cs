using RoleRollsPocketEdition.Creatures.Domain;
using RoleRollsPocketEdition.Creatures.Domain.Models;
using RoleRollsPocketEdition.CreaturesTemplates.Domain.Templates;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos
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