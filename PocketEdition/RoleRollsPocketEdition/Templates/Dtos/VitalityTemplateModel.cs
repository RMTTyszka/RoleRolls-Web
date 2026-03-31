using System.Collections.Generic;
using System.Linq;
using RoleRollsPocketEdition.Core.Entities;
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
            BasicAttackOrder = vitality.BasicAttackOrder;
            ConditionAtThirtyPercent = vitality.ConditionAtThirtyPercent;
            ConditionAtZero = vitality.ConditionAtZero;
            FormulaTokens = vitality.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Formula { get; set; }
        public int? BasicAttackOrder { get; set; }
        public Property? ConditionAtThirtyPercent { get; set; }
        public Property? ConditionAtZero { get; set; }
        public List<FormulaToken> FormulaTokens { get; set; } = new();

    }
}


