using RoleRollsPocketEdition.Creatures.Domain;

namespace RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos
{
    public class LifeTemplateModel
    {
        public LifeTemplateModel()
        {

        }
        public LifeTemplateModel(LifeTemplate life)
        {
            Id = life.Id;
            Name = life.Name;
            MaxValue = life.MaxValue;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxValue { get; set; }
    }
}