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
            Formula = life.Formula;
        }

        public string Name { get; set; }
        public string Formula { get; set; }

        internal void Update(LifeTemplateModel lifeModel)
        {
            Name = lifeModel.Name;
            Formula = lifeModel.Formula;
        }
    }
}
