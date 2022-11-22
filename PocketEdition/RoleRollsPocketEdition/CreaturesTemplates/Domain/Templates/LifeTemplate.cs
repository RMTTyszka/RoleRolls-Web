using RoleRollsPocketEdition.CreaturesTemplates.Application.Dtos;

namespace RoleRollsPocketEdition.Creatures.Domain
{
    public class LifeTemplate : Entity
    {
        public LifeTemplate()
        {

        }
        public LifeTemplate(LifeTemplateModel life)
        {
            Id = life.Id;
            Name = life.Name;
            MaxValue = life.MaxValue;
        }

        public string Name { get; set; }
        public int MaxValue { get; set; }
    }
}
