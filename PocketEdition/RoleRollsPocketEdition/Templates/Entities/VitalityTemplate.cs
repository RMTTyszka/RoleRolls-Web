using RoleRollsPocketEdition.Core.Entities;
using RoleRollsPocketEdition.Creatures.Entities;
using RoleRollsPocketEdition.Templates.Dtos;

namespace RoleRollsPocketEdition.Templates.Entities
{
    public class VitalityTemplate : Entity
    {
        public VitalityTemplate()
        {

        }
        public VitalityTemplate(VitalityTemplateModel vitality)
        {
            Id = vitality.Id;
            Name = vitality.Name;
            Formula = vitality.Formula;
        }

        public string Name { get; set; }
        public string Formula { get; set; }
        public Guid CreatureTemplateId { get; set; }
        public CampaignTemplate CampaignTemplate { get; set; }
        public ICollection<Vitality> Vitalities { get; set; }

        public void Update(VitalityTemplateModel vitalityModel)
        {
            Name = vitalityModel.Name;
            Formula = vitalityModel.Formula;
        }
    }
}
