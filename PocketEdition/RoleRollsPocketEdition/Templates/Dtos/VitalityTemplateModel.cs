using System.Collections.Generic;
using System.Linq;
using RoleRollsPocketEdition.Templates.Entities;

namespace RoleRollsPocketEdition.Templates.Dtos
{
    public class VitalityTemplateModel
    {
        public VitalityTemplateModel()
        {

        }
        public VitalityTemplateModel(VitalityTemplate vitality)
        {
            Id = vitality.Id;
            Name = vitality.Name;
            Formula = vitality.Formula;
            FormulaTokens = vitality.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public List<FormulaToken> FormulaTokens { get; set; } = new();

    }
}


