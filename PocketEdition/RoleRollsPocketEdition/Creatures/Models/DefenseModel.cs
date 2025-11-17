using RoleRollsPocketEdition.Creatures.Entities;

namespace RoleRollsPocketEdition.Creatures.Models
{
    public class DefenseModel
    {

        public DefenseModel()
        {

        }
        public Guid Id { get; set; }
        public int Value { get; set; }
        public string Name { get; set; }
        public Guid DefenseTemplateId { get; set; }
        public string? FormulaDescription { get; set; }
        public string? FormulaExpression { get; set; }

        public DefenseModel(Defense defense, Creature creature)
        {
            Id = defense.Id;
            DefenseTemplateId = defense.DefenseTemplateId;
            var formulaDetails = creature.GetFormulaDetails(defense.Formula, defense.FormulaTokens);
            Value = formulaDetails.Value;
            FormulaDescription = formulaDetails.Description;
            FormulaExpression = formulaDetails.ResolvedFormula;
            Name = defense.Name;
        }
    }
}


