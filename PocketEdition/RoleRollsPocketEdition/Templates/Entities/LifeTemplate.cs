using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
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
        public Guid CreatureTemplateId { get; set; }
        public CampaignTemplate CampaignTemplate { get; set; }
        public ICollection<Life> Lifes { get; set; }

        public void Update(LifeTemplateModel lifeModel)
        {
            Name = lifeModel.Name;
            Formula = lifeModel.Formula;
        }
    }
}
