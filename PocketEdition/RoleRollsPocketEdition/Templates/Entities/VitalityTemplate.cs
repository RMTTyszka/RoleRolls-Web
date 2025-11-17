using System.Collections.Generic;
using System.Linq;
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
            FormulaTokens = vitality.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
        }

        public string Name { get; set; }
        public string Formula { get; set; }
        public List<FormulaToken> FormulaTokens { get; set; } = new();
        public Guid CampaignTemplateId { get; set; }
        public CampaignTemplate CampaignTemplate { get; set; }
        public ICollection<Vitality> Vitalities { get; set; }

        public void Update(VitalityTemplateModel vitalityModel)
        {
            Name = vitalityModel.Name;
            Formula = vitalityModel.Formula;
            FormulaTokens = vitalityModel.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
        }
    }
}


