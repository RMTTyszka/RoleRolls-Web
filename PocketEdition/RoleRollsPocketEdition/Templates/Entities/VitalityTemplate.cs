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
            BasicAttackOrder = vitality.BasicAttackOrder;
            ConditionAtThirtyPercent = vitality.ConditionAtThirtyPercent;
            ConditionAtZero = vitality.ConditionAtZero;
            FormulaTokens = vitality.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
        }

        public string Name { get; set; }
        public string Formula { get; set; }
        public int? BasicAttackOrder { get; set; }
        public Property? ConditionAtThirtyPercent { get; set; }
        public Property? ConditionAtZero { get; set; }
        public List<FormulaToken> FormulaTokens { get; set; } = new();
        public Guid CampaignTemplateId { get; set; }
        public CampaignTemplate CampaignTemplate { get; set; }
        public ICollection<Vitality> Vitalities { get; set; }

        public void Update(VitalityTemplateModel vitalityModel)
        {
            Name = vitalityModel.Name;
            Formula = vitalityModel.Formula;
            BasicAttackOrder = vitalityModel.BasicAttackOrder;
            ConditionAtThirtyPercent = vitalityModel.ConditionAtThirtyPercent;
            ConditionAtZero = vitalityModel.ConditionAtZero;
            FormulaTokens = vitalityModel.FormulaTokens?.Select(token => token.Clone()).ToList() ?? new List<FormulaToken>();
        }

        public BasicAttackVitalityRule? ToBasicAttackVitalityRule(
            CreatureCondition? conditionAtThirtyPercent,
            CreatureCondition? conditionAtZero)
        {
            if (!BasicAttackOrder.HasValue || BasicAttackOrder <= 0)
            {
                return null;
            }

            return new BasicAttackVitalityRule
            {
                Vitality = new Property(Id, PropertyType.Vitality),
                ConditionAtThirtyPercent = conditionAtThirtyPercent == null
                    ? null
                    : new BasicAttackConditionRule
                    {
                        Condition = new Property(conditionAtThirtyPercent.Id, PropertyType.CreatureCondition),
                        Name = conditionAtThirtyPercent.Name,
                        Description = conditionAtThirtyPercent.Description
                    },
                ConditionAtZero = conditionAtZero == null
                    ? null
                    : new BasicAttackConditionRule
                    {
                        Condition = new Property(conditionAtZero.Id, PropertyType.CreatureCondition),
                        Name = conditionAtZero.Name,
                        Description = conditionAtZero.Description
                    }
            };
        }
    }
}


