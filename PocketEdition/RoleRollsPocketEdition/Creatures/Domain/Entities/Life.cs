using RoleRollsPocketEdition.CreaturesTemplates.Domain.Templates;
using RoleRollsPocketEdition.Global;

namespace RoleRollsPocketEdition.Creatures.Domain.Entities
{
    public class Life : Entity
    {
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid LifeTemplateId { get; set; }
        public Life(LifeTemplate life)
        {
            Id = Guid.NewGuid();
            LifeTemplateId = life.Id;
            // TODO formula
            Name = life.Name;
        }

        public Life()
        {
        }

    }
   
}