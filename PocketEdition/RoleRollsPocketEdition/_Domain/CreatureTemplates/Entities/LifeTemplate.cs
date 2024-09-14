using RoleRollsPocketEdition._Application.CreaturesTemplates.Dtos;
using RoleRollsPocketEdition.Core;

namespace RoleRollsPocketEdition._Domain.CreatureTemplates.Entities
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

        public void Update(LifeTemplateModel lifeModel)
        {
            Name = lifeModel.Name;
            Formula = lifeModel.Formula;
        }
    }
}
