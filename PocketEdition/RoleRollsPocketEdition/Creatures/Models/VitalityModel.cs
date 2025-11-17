using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Models
{
    public class VitalityModel
    {

        public VitalityModel()
        {

        }
        public Guid Id { get; set; }
        public int MaxValue { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid VitalityTemplateId { get; set; }
        public string? FormulaDescription { get; set; }
        public string? FormulaExpression { get; set; }

        public VitalityModel(Vitality vitality, Creature creature)
        {
            Id = vitality.Id;
            VitalityTemplateId = vitality.VitalityTemplateId;
            MaxValue = vitality.MaxValue;
            Value = vitality.Value;
            Name = vitality.Name;
            var formulaDetails = creature.GetFormulaDetails(vitality.VitalityTemplate.Formula,
                vitality.VitalityTemplate.FormulaTokens);
            FormulaDescription = formulaDetails.Description;
            FormulaExpression = formulaDetails.ResolvedFormula;
        }
    }
}


